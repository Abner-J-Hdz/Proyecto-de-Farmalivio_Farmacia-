using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmalivio_Farmacia.Models;
using Farmalivio_Farmacia.helpers;
using Farmalivio_Farmacia.ViewModel;
using System.Web.Routing;
using Farmalivio_Farmacia.Filter;

namespace Farmalivio_Farmacia.Controllers
{
    public class PatologiasController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Patologias
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new PatologiaViewModel();
            if (busqueda != null)
            {
                var Patology = db.patologia.Where(x => x.nombre_patologia.StartsWith(busqueda)).OrderByDescending(x => x.id_patologia).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.patologia.Where(x => x.nombre_patologia.StartsWith(busqueda)).Count();

                modelo.Patologias = Patology;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Patology = db.patologia.OrderByDescending(x => x.id_patologia).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.patologia.Count();
                modelo.Patologias = Patology;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }

        }

        // GET: Patologias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patologia patologia = db.patologia.Find(id);
            if (patologia == null)
            {
                return HttpNotFound();
            }
            return View(patologia);
        }

        // GET: Patologias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patologias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create([Bind(Include = "id_patologia,nombre_patologia,descripcion_patologia")] patologia patologia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    patologia.estado = true;
                    db.patologia.Add(patologia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarPatologia(patologia.nombre_patologia);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorPatologia", "Ya existe una patologia con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorPatologia", "Ocurrio un error al guadar");
                    }
                }

            }

            return View(patologia);
        }

        // GET: Patologias/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patologia patologia = db.patologia.Find(id);
            if (patologia == null)
            {
                return HttpNotFound();
            }
            return View(patologia);
        }

        // POST: Patologias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit([Bind(Include = "id_patologia,nombre_patologia,descripcion_patologia,estado")] patologia patologia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var modelo = db.patologia.Where(x => x.id_patologia == patologia.id_patologia).SingleOrDefault();
                    modelo.nombre_patologia = patologia.nombre_patologia;
                    modelo.descripcion_patologia = patologia.descripcion_patologia;

                    db.Entry(modelo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarPatologia(patologia.nombre_patologia);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorPatologia", "Ya existe una patologia con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorPatologia", "Ocurrio un error al guadar");
                    }
                }

            }
            return View(patologia);
        }

        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new patologia();
            try
            {
                obtener = db.patologia.Where(x => x.id_patologia == ID).SingleOrDefault();
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
                //throw;
            }

            return RedirectToAction("Index");

        }

        //// GET: Patologias/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    patologia patologia = db.patologia.Find(id);
        //    if (patologia == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(patologia);
        //}

            //// POST: Patologias/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public ActionResult DeleteConfirmed(int id)
            //{
            //    patologia patologia = db.patologia.Find(id);
            //    db.patologia.Remove(patologia);
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
