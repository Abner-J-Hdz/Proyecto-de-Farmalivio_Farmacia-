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
    public class MedidasController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Medidas
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new MedidaViewModel();
            if (busqueda != null)
            {
                var UMedida = db.umedida.Where(x => x.nombre_umedida.StartsWith(busqueda)).OrderByDescending(x => x.id_umedida).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.umedida.Where(x => x.nombre_umedida.StartsWith(busqueda)).Count();

                modelo.Medidas = UMedida;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var UMedida = db.umedida.OrderByDescending(x => x.id_umedida).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.umedida.Count();
                modelo.Medidas = UMedida;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }
        }

        // GET: Medidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            umedida umedida = db.umedida.Find(id);
            if (umedida == null)
            {
                return HttpNotFound();
            }
            return View(umedida);
        }

        // GET: Medidas/Create
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create([Bind(Include = "id_umedida,nombre_umedida,descripcion_umedida")] umedida umedida)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    umedida.estado = true;
                    db.umedida.Add(umedida);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarMedida(umedida.nombre_umedida);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorMedida", "Ya existe una medida con este nombre, agruegue una diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorMedida", "Ocurrio un error al guadar");
                    }
                }
            }

            return View(umedida);
        }

        // GET: Medidas/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            umedida umedida = db.umedida.Find(id);
            if (umedida == null)
            {
                return HttpNotFound();
            }
            return View(umedida);
        }

        // POST: Medidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_umedida,nombre_umedida,descripcion_umedida,estado")] umedida umedida)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = db.umedida.Where(x => x.id_umedida == umedida.id_umedida).SingleOrDefault();
                    model.nombre_umedida = umedida.nombre_umedida;
                    model.descripcion_umedida = umedida.descripcion_umedida;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarMedida(umedida.nombre_umedida);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorMedida", "Ya existe una medida con este nombre, agruegue una diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorMedida", "Ocurrio un error al actualizar");
                    }
                }

            }
            return View(umedida);
        }

        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new umedida();
            try
            {
                obtener = db.umedida.Where(x => x.id_umedida == ID).SingleOrDefault();
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

        //// GET: Medidas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    umedida umedida = db.umedida.Find(id);
        //    if (umedida == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(umedida);
        //}

        //// POST: Medidas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    umedida umedida = db.umedida.Find(id);
        //    db.umedida.Remove(umedida);
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
