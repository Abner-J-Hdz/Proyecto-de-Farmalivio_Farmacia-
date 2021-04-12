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
    public class PresentacionesController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Presentaciones
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new PresentacionViewModel();
            if (busqueda != null)
            {
                var Presentation = db.presentacion.Where(x => x.nombre_presentacion.StartsWith(busqueda)).OrderByDescending(x => x.id_presentacion).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.presentacion.Where(x => x.nombre_presentacion.StartsWith(busqueda)).Count();

                modelo.Presentaciones = Presentation;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Presentation = db.presentacion.OrderByDescending(x => x.id_presentacion).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.presentacion.Count();
                modelo.Presentaciones = Presentation;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }
        }

        // GET: Presentaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presentacion presentacion = db.presentacion.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            return View(presentacion);
        }

        // GET: Presentaciones/Create
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Presentaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create([Bind(Include = "id_presentacion,nombre_presentacion,descripcion_presentacion")] presentacion presentacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    presentacion.estado = true;
                    db.presentacion.Add(presentacion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarPresentacion(presentacion.nombre_presentacion);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorPresentacion", "Ya existe una presentacion con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorPresentacion", "Ocurrio un error al guadar");
                    }
                }
            }
            return View(presentacion);
        }

        // GET: Presentaciones/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presentacion presentacion = db.presentacion.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            return View(presentacion);
        }

        // POST: Presentaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit([Bind(Include = "id_presentacion,nombre_presentacion,descripcion_presentacion")] presentacion presentacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = db.presentacion.Where(x => x.id_presentacion == presentacion.id_presentacion).SingleOrDefault();
                    model.nombre_presentacion = presentacion.nombre_presentacion;
                    model.descripcion_presentacion = presentacion.descripcion_presentacion;

                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarPresentacion(presentacion.nombre_presentacion);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorPresentacion", "Ya existe una presentacion con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorPresentacion", "Ocurrio un error al guadar");
                    }
                }

            }
            return View(presentacion);
        }
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new presentacion();
            try
            {
                obtener = db.presentacion.Where(x => x.id_presentacion == ID).SingleOrDefault();
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

        // GET: Presentaciones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    presentacion presentacion = db.presentacion.Find(id);
        //    if (presentacion == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(presentacion);
        //}

        //// POST: Presentaciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    presentacion presentacion = db.presentacion.Find(id);
        //    db.presentacion.Remove(presentacion);
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
