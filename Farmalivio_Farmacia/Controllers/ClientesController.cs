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
    public class ClientesController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Clientes
        [AuthorizeUser(rol: "Vendedor", catalogo: "Cliente")]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new ClienteViewModel();
            if (busqueda != null)
            {
                var Clien = db.cliente.Where(x => x.nombre_cliente.StartsWith(busqueda)).OrderByDescending(x => x.id_cliente).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.cliente.Where(x => x.nombre_cliente.StartsWith(busqueda)).Count();

                modelo.Clientes = Clien;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Clien = db.cliente.OrderByDescending(x => x.id_cliente).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.cliente.Count();
                modelo.Clientes = Clien;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }
        }

        // GET: Clientes/Details/5
        [AuthorizeUser(rol: "Vendedor", catalogo: "Cliente")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        [AuthorizeUser(rol: "Vendedor", catalogo: "Cliente")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Cliente")]
        public ActionResult Create([Bind(Include = "id_cliente,nombre_cliente,apellido_cliente,direccion,cedula")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (cliente.cedula != null)
                    {
                        VerificacionDuplicado verif = new VerificacionDuplicado();
                        bool rptacedula;
                        rptacedula = verif.VerificarCliente(cliente.cedula , 0);
                        if (rptacedula==false)
                        {
                            cliente.estado = true;
                            db.cliente.Add(cliente);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("cedula", "Ya existe un cliente con esta cedula, agruegue una diferente");
                        }
                    }
                    else
                    {
                        cliente.estado = true;
                        db.cliente.Add(cliente);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception)
                {
                    ModelState.AddModelError("erroCliente", "Ocurrio un errror al guardar");
                }

            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [AuthorizeUser(rol: "Vendedor", catalogo: "Cliente")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Cliente")]
        public ActionResult Edit([Bind(Include = "id_cliente,nombre_cliente,apellido_cliente,direccion,cedula")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rptacedula;
                    rptacedula = verif.VerificarCliente(cliente.cedula, cliente.id_cliente);
                    var model = new cliente();

                    model = db.cliente.Where(x => x.id_cliente == cliente.id_cliente).SingleOrDefault();

                    if (model.cedula!=null && cliente.cedula==null)
                    {
                        ModelState.AddModelError("cedula", "No puede dejar este campo vacio");
                    }
                    else
                    {
                        if (rptacedula==false)
                        {
                            model.nombre_cliente = cliente.nombre_cliente;
                            model.apellido_cliente = cliente.apellido_cliente;
                            model.direccion = cliente.direccion;
                            model.cedula = cliente.cedula;

                            db.Entry(model).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");

                        }
                        else { ModelState.AddModelError("cedula", "Ya existe un cliente con esta cedula, agruegue una diferente"); }
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("errorCliente", "Ocurrio un errror al guardar");
                }

            }
            return View(cliente);
        }
        [AuthorizeUser(rol: "Vendedor", catalogo: "Cliente")]
        public ActionResult Changestate(int id)
        {
            if (id!=1)
            {
                int ID = id;
                var obtener = new cliente();
                try
                {
                    obtener = db.cliente.Where(x => x.id_cliente == ID).SingleOrDefault();
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
                {//return RedirectToAction("Index");//throw; 
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    cliente cliente = db.cliente.Find(id);
        //    if (cliente == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cliente);
        //}

        //// POST: Clientes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    cliente cliente = db.cliente.Find(id);
        //    db.cliente.Remove(cliente);
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
