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
    public class LaboratoriosController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Laboratorios
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new LaboratorioViewModel();
            if (busqueda!=null)
            {
                var Laboratory = db.laboratorio.Where(x => x.nombre_laboratorio.StartsWith(busqueda)).OrderByDescending(x => x.id_laboratorio).
                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.laboratorio.Where(x => x.nombre_laboratorio.StartsWith(busqueda)).Count();

                modelo.Laboratorios = Laboratory;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Laboratory = db.laboratorio.OrderByDescending(x => x.id_laboratorio).
                     Skip((pagina - 1) * cantidadRegistrosPorPagina).
                     Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.laboratorio.Count();
                modelo.Laboratorios = Laboratory;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }
        }

        // GET: Laboratorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            laboratorio laboratorio = db.laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // GET: Laboratorios/Create
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laboratorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create([Bind(Include = "id_laboratorio,nombre_laboratorio,descripcion_laboratorio,estado")] laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    laboratorio.estado = true;
                    db.laboratorio.Add(laboratorio);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarLaboratorio(laboratorio.nombre_laboratorio);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorLaboratorio", "Ya existe una Laboratorio con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorLaboratorio", "Ocurrio un error al guadar");
                    }
                }
            }

            return View(laboratorio);
        }

        // GET: Laboratorios/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            laboratorio laboratorio = db.laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit([Bind(Include = "id_laboratorio,nombre_laboratorio,descripcion_laboratorio,estado")] laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = db.laboratorio.Where(x => x.id_laboratorio == laboratorio.id_laboratorio).SingleOrDefault();
                    model.nombre_laboratorio = laboratorio.nombre_laboratorio;
                    model.descripcion_laboratorio = laboratorio.descripcion_laboratorio;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarLaboratorio(laboratorio.nombre_laboratorio);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorLaboratorio", "Ya existe una Laboratorio con este nombre, agruegue uno diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorLaboratorio", "Ocurrio un error al actualizar");
                    }
                }

            }
            return View(laboratorio);
        }

        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Changestate(int id) {
            int ID = id;
            var obtener = new laboratorio();
            try
            {
                obtener = db.laboratorio.Where(x => x.id_laboratorio == ID).SingleOrDefault();
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


        // GET: Laboratorios/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    laboratorio laboratorio = db.laboratorio.Find(id);
        //    if (laboratorio == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(laboratorio);
        //}

        //// POST: Laboratorios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    laboratorio laboratorio = db.laboratorio.Find(id);
        //    db.laboratorio.Remove(laboratorio);
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
