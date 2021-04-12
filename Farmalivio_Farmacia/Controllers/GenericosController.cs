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
    public class GenericosController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Genericos
        [AuthorizeUser(rol: "Vendedor", catalogo: "Generico")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new GenericoViewModel();
            if (busqueda != null)
            {
                var Genericoes = db.generico.Where(x => x.nombre_generico.StartsWith(busqueda)).OrderByDescending(x => x.id_generico).
                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.generico.Where(x => x.nombre_generico.StartsWith(busqueda)).Count();

                modelo.Genericos = Genericoes;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Genericoes = db.generico.OrderByDescending(x => x.id_generico).
                                Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.generico.Count();

                modelo.Genericos = Genericoes;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }
        }

        // GET: Genericos/Details/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            generico generico = db.generico.Find(id);
            if (generico == null)
            {
                return HttpNotFound();
            }
            return View(generico);
        }

        // GET: Genericos/Create
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genericos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Create([Bind(Include = "id_generico,nombre_generico,descripcion_generico")] generico generico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    generico.estado = true;
                    db.generico.Add(generico);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarGenerico(generico.nombre_generico);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorGenerico", "Ya existe un generico con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorGenerico", "Ocurrio un error al guadar");
                    }
                }
            }

            return View(generico);
        }

        // GET: Genericos/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            generico generico = db.generico.Find(id);
            if (generico == null)
            {
                return HttpNotFound();
            }
            return View(generico);
        }

        // POST: Genericos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Edit([Bind(Include = "id_generico,nombre_generico,descripcion_generico")] generico generico)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var model = db.generico.Where(x => x.id_generico == generico.id_generico).SingleOrDefault();
                    model.nombre_generico = generico.nombre_generico;
                    model.descripcion_generico = generico.descripcion_generico;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarGenerico(generico.nombre_generico);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorGenerico", "Ya existe un generico con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorGenerico", "Ocurrio un error al guadar");
                    }
                }

            }
            return View(generico);
        }
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new generico();
            try
            {
                obtener = db.generico.Where(x => x.id_generico == ID).SingleOrDefault();
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


        // GET: Genericos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    generico generico = db.generico.Find(id);
        //    if (generico == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(generico);
        //}

        //// POST: Genericos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    generico generico = db.generico.Find(id);
        //    db.generico.Remove(generico);
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
