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
    public class CategoriasController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Categorias
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new CategoriaViewModel();
            if (busqueda!=null)
            {
                var Categories = db.categoria.Where(x => x.nombre_categoria.StartsWith(busqueda)).OrderByDescending(x => x.id_categoria).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.categoria.Where(x => x.nombre_categoria.StartsWith(busqueda)).Count();

                modelo.Categorias = Categories;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Categories = db.categoria.OrderByDescending(x => x.id_categoria).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.categoria.Count();
                modelo.Categorias = Categories;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }

        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoria = db.categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Create([Bind(Include = "id_categoria,nombre_categoria,descripcion_categoria")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                categoria.estado = true;
                db.categoria.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarCategoria(categoria.nombre_categoria);
                    if (rpta==true)
                    {
                        ModelState.AddModelError("errorCategoria", "Ya existe una categoria con este nombre, agruegue una diferente");
                    }
                    else
                    {
                        ModelState.AddModelError("errorCategoria", "Ocurrio un error al guadar");
                    }
                    
                }

            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoria = db.categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Edit([Bind(Include = "id_categoria,nombre_categoria,descripcion_categoria")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = db.categoria.Where(x => x.id_categoria == categoria.id_categoria).SingleOrDefault();
                    model.nombre_categoria = categoria.nombre_categoria;
                    model.descripcion_categoria = categoria.descripcion_categoria;
                    db.SaveChanges();

                    //db.Entry(categoria).State = EntityState.Modified;

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta;
                    rpta = verif.VerificarCategoria(categoria.nombre_categoria);
                    if (rpta == true)
                    {
                        ModelState.AddModelError("errorCategoria", "Ya existe una categoria con este nombre");
                    }
                    else
                    {
                        ModelState.AddModelError("errorCategoria", "Ocurrio un error al actualizar");
                    }
                }

            }
            return View(categoria);
        }

        [AuthorizeUser(rol: "Administrador", catalogo: "Categorias")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new categoria();
            try
            {
                obtener = db.categoria.Where(x=> x.id_categoria == ID).SingleOrDefault();
                if (obtener!=null)
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


        // GET: Categorias/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    categoria categoria = db.categoria.Find(id);
        //    if (categoria == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(categoria);
        //}

        //// POST: Categorias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    categoria categoria = db.categoria.Find(id);
        //    db.categoria.Remove(categoria);
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
