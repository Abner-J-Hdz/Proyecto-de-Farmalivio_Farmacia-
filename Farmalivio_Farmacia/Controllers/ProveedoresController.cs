using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmalivio_Farmacia.Models;
using Farmalivio_Farmacia.ViewModel;
using System.Web.Routing;
using Farmalivio_Farmacia.helpers;
using Farmalivio_Farmacia.Filter;

namespace Farmalivio_Farmacia.Controllers
{
    public class ProveedoresController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Proveedores
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new ProveedoresViewModel();
            if (busqueda != null)
            {
                var Prov = db.proveedor.Where(x => x.nombre_proveedor.StartsWith(busqueda)).OrderByDescending(x => x.id_proveedor).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.proveedor.Where(x => x.nombre_proveedor.StartsWith(busqueda)).Count();

                modelo.Proveedores = Prov;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Prov = db.proveedor.OrderByDescending(x => x.id_proveedor).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.proveedor.Count();
                modelo.Proveedores = Prov;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedores/Create
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create([Bind(Include = "id_proveedor,nombre_proveedor,direccion,ruc")] proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    proveedor.estado = true;
                        db.proveedor.Add(proveedor);
                        db.SaveChanges();
                        return RedirectToAction("Index");
     
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rptanombre, rptaruc;
                    rptanombre = verif.VerificarProveedor(proveedor.nombre_proveedor);
                    if (rptanombre == true)
                    {
                        ModelState.AddModelError("errorProveedor", "Ya existe una proveedor con este nombre, agruegue una diferente");
                    }

                    rptaruc = verif.VerificarProveedorRuc(proveedor.ruc);
                    if (rptaruc==true)
                    {
                        ModelState.AddModelError("ruc", "Ya existe un proveedor con este ruc, agruegue una diferente");
                    }
                    if (rptanombre==false && rptaruc==false)
                    {
                        ModelState.AddModelError("errorProveedor", "Ocurrio un error al guadar");
                    }
                }
            }

            return View(proveedor);
        }

        // GET: Proveedores/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit([Bind(Include = "id_proveedor,nombre_proveedor,direccion,ruc")] proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                        var model = db.proveedor.Where(x => x.id_proveedor == proveedor.id_proveedor).SingleOrDefault();
                        model.nombre_proveedor = proveedor.nombre_proveedor;
                        model.direccion = proveedor.direccion;
                        model.ruc = proveedor.ruc;

                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    bool rptaruc = verif.VerificarProveedorRuc(proveedor.ruc);
                    rpta = verif.VerificarProveedor(proveedor.nombre_proveedor);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorProveedor", "Ya existe un proveedor con este nombre, agruegue uno diferente");
                    }
                    
                    if (rptaruc)
                    {
                        ModelState.AddModelError("ruc", "Ya existe un proveedor con este ruc, agruegue una diferente");
                        
                    }
                    if (rpta==false && rptaruc==false)
                    {
                        ModelState.AddModelError("errorProveedor", "Ocurrio un error al Actualizar");
                    }
                    
                }

            }
            return View(proveedor);
        }
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new proveedor();
            try
            {
                obtener = db.proveedor.Where(x => x.id_proveedor == ID).SingleOrDefault();
                if (obtener != null)
                {
                    if (obtener.estado == true)
                    {
                        obtener.estado = false;
                        db.Entry(obtener).State = EntityState.Modified;
                    }
                    else
                    {
                        obtener.estado = true;
                        db.Entry(obtener).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                //return RedirectToAction("Index");
                //throw;
            }

            return RedirectToAction("Index");
        }

        //// GET: Proveedores/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    proveedor proveedor = db.proveedor.Find(id);
        //    if (proveedor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(proveedor);
        //}

        //// POST: Proveedores/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    proveedor proveedor = db.proveedor.Find(id);
        //    db.proveedor.Remove(proveedor);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
