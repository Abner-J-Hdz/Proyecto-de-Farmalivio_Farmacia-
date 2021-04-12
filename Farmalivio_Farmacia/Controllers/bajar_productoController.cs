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

namespace Farmalivio_Farmacia.Controllers
{
    public class bajar_productoController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: bajar_producto
        public ActionResult Index(int pagina = 1)
        {
            //var bajar_producto = db.bajar_producto.Include(b => b.detalle_producto).Include(b=>b.detalle_producto.producto).Include(b => b.motivo).Include(b => b.usuario);
            //return View(bajar_producto.ToList());
            var cantidadRegistrosPorPagina = 10;
            var modelo = new BajasViewModel();

            var BajasProd = db.bajar_producto.Include(b => b.detalle_producto).Include(b => b.detalle_producto.producto).Include(b => b.motivo).Include(b => b.usuario).OrderByDescending(x=>x.fecha).
                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                    Take(cantidadRegistrosPorPagina).ToList();
            var totalDeRegistros = db.categoria.Count();
            modelo.Bajas = BajasProd;
            modelo.PaginaActual = pagina;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.ValoresQueryString = new RouteValueDictionary();
            return View(modelo);
        }

        // GET: bajar_producto/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    bajar_producto bajar_producto = db.bajar_producto.Find(id);
        //    if (bajar_producto == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bajar_producto);
        //}

        // GET: bajar_producto/Create
        //public ActionResult Create()
        //{
        //    ViewBag.id_detalle_producto = new SelectList(db.detalle_producto, "id_detalle_producto", "id_detalle_producto");
        //    ViewBag.id_motivo = new SelectList(db.motivo, "id_motivo", "nombre_motivo");
        //    ViewBag.id_usuario = new SelectList(db.usuario, "id_usuario", "nombre_user");
        //    return View();
        //}

        // POST: bajar_producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_bajar_producto,id_detalle_producto,cantidad,id_usuario,id_motivo,fecha")] bajar_producto bajar_producto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.bajar_producto.Add(bajar_producto);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.id_detalle_producto = new SelectList(db.detalle_producto, "id_detalle_producto", "id_detalle_producto", bajar_producto.id_detalle_producto);
        //    ViewBag.id_motivo = new SelectList(db.motivo, "id_motivo", "nombre_motivo", bajar_producto.id_motivo);
        //    ViewBag.id_usuario = new SelectList(db.usuario, "id_usuario", "nombre_user", bajar_producto.id_usuario);
        //    return View(bajar_producto);
        //}

        // GET: bajar_producto/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    bajar_producto bajar_producto = db.bajar_producto.Find(id);
        //    if (bajar_producto == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.id_detalle_producto = new SelectList(db.detalle_producto, "id_detalle_producto", "id_detalle_producto", bajar_producto.id_detalle_producto);
        //    ViewBag.id_motivo = new SelectList(db.motivo, "id_motivo", "nombre_motivo", bajar_producto.id_motivo);
        //    ViewBag.id_usuario = new SelectList(db.usuario, "id_usuario", "nombre_user", bajar_producto.id_usuario);
        //    return View(bajar_producto);
        //}

        // POST: bajar_producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id_bajar_producto,id_detalle_producto,cantidad,id_usuario,id_motivo,fecha")] bajar_producto bajar_producto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bajar_producto).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_detalle_producto = new SelectList(db.detalle_producto, "id_detalle_producto", "id_detalle_producto", bajar_producto.id_detalle_producto);
        //    ViewBag.id_motivo = new SelectList(db.motivo, "id_motivo", "nombre_motivo", bajar_producto.id_motivo);
        //    ViewBag.id_usuario = new SelectList(db.usuario, "id_usuario", "nombre_user", bajar_producto.id_usuario);
        //    return View(bajar_producto);
        //}

        // GET: bajar_producto/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    bajar_producto bajar_producto = db.bajar_producto.Find(id);
        //    if (bajar_producto == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bajar_producto);
        //}

        // POST: bajar_producto/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    bajar_producto bajar_producto = db.bajar_producto.Find(id);
        //    db.bajar_producto.Remove(bajar_producto);
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
