using Farmalivio_Farmacia.Models;
using Farmalivio_Farmacia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Farmalivio_Farmacia.Controllers
{
    public class DevolucionesController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();
        // GET: Devoluciones
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult Create(string criterio, string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new ProdDevolverViewModel();
            if (busqueda != null)
            {
                var Prod = db.vista_prod_devolver.Where(x => x.nombre_proveedor.StartsWith(busqueda) && x.stock > x.undCajasProd).OrderBy(x => x.nombre_producto).
                Skip((pagina - 1) * cantidadRegistrosPorPagina).
                Take(cantidadRegistrosPorPagina).ToList();

                var totalDeRegistros = db.vista_prod_devolver.Where(x => x.nombre_proveedor.StartsWith(busqueda) && x.stock > x.undCajasProd).Count();
                modelo.ProductosDevolver = Prod;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["/create"] = "/create";
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);                
            }
            else
            {
                var Prod = db.vista_prod_devolver.Where(x => x.nombre_proveedor.StartsWith(busqueda) && x.id_detalle_producto == 0).OrderBy(x => x.nombre_producto).
                                                  Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                                Take(cantidadRegistrosPorPagina).ToList();

                var totalDeRegistros = db.vista_prod_devolver.Where(x => x.nombre_proveedor.StartsWith(busqueda) && x.id_detalle_producto == 0).Count();

                modelo.ProductosDevolver = Prod;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["/create?busqueda"] = busqueda;
                return View(modelo);
            }
        }

        public ActionResult Guardar()
        {

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Anular()
        {
            return RedirectToAction("Index");
        }

        public JsonResult ObtenerDatos(string criterio, string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new ProdDevolverViewModel();
            var Prod = db.vista_prod_devolver.OrderBy(x => x.nombre_producto).
                                  Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                Take(cantidadRegistrosPorPagina).ToList();

            var totalDeRegistros = db.vista_prod_devolver.Count();

            modelo.ProductosDevolver = Prod;
            modelo.PaginaActual = pagina;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.ValoresQueryString = new RouteValueDictionary();
            modelo.ValoresQueryString["/create?busqueda"] = busqueda;
            return Json(modelo, JsonRequestBehavior.AllowGet);
        }


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