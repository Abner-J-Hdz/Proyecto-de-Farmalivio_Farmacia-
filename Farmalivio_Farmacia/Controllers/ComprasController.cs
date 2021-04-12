using Farmalivio_Farmacia.Filter;
using Farmalivio_Farmacia.helpers;
using Farmalivio_Farmacia.Models;
using Farmalivio_Farmacia.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Farmalivio_Farmacia.Controllers
{
    public class ComprasController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();
        // GET: Compras
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Index(string criterio, string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            var modelo = new CompraViewModel();
            if (busqueda != null)
            {
                var Shops = db.vista_Compra_listado.Where(x => x.nombre_proveedor.StartsWith(busqueda)).OrderByDescending(x => x.id_compra).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.vista_Compra_listado.Where(x => x.nombre_proveedor.StartsWith(busqueda)).Count();

                modelo.Compras = Shops;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["busqueda"] = busqueda;
                return View(modelo);
            }
            else
            {
                var Shops = db.vista_Compra_listado.OrderByDescending(x => x.id_compra).
                                    Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                    Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.vista_Compra_listado.Count();
                modelo.Compras = Shops;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString[""] = busqueda;
                return View(modelo);
            }
        }

        [HttpGet]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Create(int id_proveedor, string numerofactura, DateTime fecha, decimal iva, decimal subtotal, decimal descuento, detalle_compra[] detalle)
        {
            //string result = "Error! No se pudo guardar!";
            string result = "LLego al controlador";
            var UserLogin = (usuario)Session["User"];
            int idUser = Convert.ToInt32(UserLogin.id_usuario);
            decimal iva15 = Convert.ToDecimal(0.15);


            var SaveCompra = new compra
            {
                id_proveedor = id_proveedor,
                id_usuario = idUser,
                fecha = fecha,
                num_fact = numerofactura,
                iva = iva,
                subtotal = subtotal,
                descuento = descuento,
                Total = (subtotal + iva)-descuento,
                id_estado = 1,
            };
            try
            {                
                db.compra.Add(SaveCompra);
                foreach (var d in detalle)
                {
                    var SaveDetail = new detalle_compra()
                    {
                        id_producto = d.id_producto,
                        cantidad_cajas = d.cantidad_cajas,
                        precio_compra = d.precio_compra,
                        undxcajas = d.undxcajas,
                        total_und = d.cantidad_cajas * d.undxcajas,
                        enexposicion = d.enexposicion,
                        descuento = d.descuento,
                       // iva = (d.precio_compra * d.cantidad_cajas) * (iva15),
                        id_lote = d.id_lote,
                        estado = true,
                    };
                    //SaveDetail.id_compra = d.id_producto;
                    //SaveDetail.cantidad_cajas = d.cantidad_cajas;
                    //SaveDetail.precio_compra = d.precio_compra;
                    //SaveDetail.undxcajas = d.undxcajas;
                    //SaveDetail.total_und = d.total_und;
                    //SaveDetail.enexposicion = d.enexposicion;
                    //SaveDetail.descuento = d.descuento;
                    //SaveDetail.id_lote = d.id_lote;
                    //SaveDetail.estado = true;
                    if (iva!=0)
                    {
                        SaveDetail.iva = (d.precio_compra * d.cantidad_cajas) * (iva15);
                    }
                    else
                    {
                        SaveDetail.iva = 0;
                    }
                    db.detalle_compra.Add(SaveDetail);
                }

                //foreach (var d in detalle)
                //{
                //    var SaveDetailProduct = new detalle_producto
                //    {
                //        id_producto = d.id_producto,
                //        id_lote = d.id_lote,
                //        numero_caja = d.cantidad_cajas,
                //        undxcajas = d.undxcajas,
                //        enexposicion = d.enexposicion,
                //        estado = true,
                //        iva = (d.precio_compra * d.cantidad_cajas) * (iva15),
                //        precio_compra = d.precio_compra,
                //        stock = d.cantidad_cajas * d.undxcajas,
                //    };
                //    if (iva != 0)
                //    {
                //        SaveDetailProduct.iva = (d.precio_compra * d.cantidad_cajas) * (iva15);
                //    }
                //    else
                //    {
                //        SaveDetailProduct.iva = 0;
                //    }
                //        db.detalle_producto.Add(SaveDetailProduct);
                //}

                db.SaveChanges();
                result = "La compra se guardado correcatmente";
            }
            catch (Exception)
            {
                throw;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Anular(int? id)
        {
            if (id!=null)
            {
                int comprobacion = 0;
                //seleccionamos los id del detalle de compra para darle de baja,posteriormente
                var shop = (from detalle in db.detalle_compra
                            where detalle.id_compra == id
                            select new
                            {
                                id_detalle = detalle.id_detalle_compra
                            }).ToArray();
                detalle_compra dc = new detalle_compra();
                detalle_producto dp = new detalle_producto();
                int can_dc, can_dp;
                //solo para de baja a la compra pero no sirve de nada  porque por que al final lo elimino
                compra compra = db.compra.Where(x => x.id_compra == id).SingleOrDefault();
                compra.id_estado = 2;//doy de baja la compra
                foreach (var item in shop)
                {
                    dc = db.detalle_compra.Where(x => x.id_compra == id && x.id_detalle_compra == item.id_detalle).SingleOrDefault();
                    dp = db.detalle_producto.Where(x => x.id_compra == id && x.id_detalle_compra == item.id_detalle).SingleOrDefault();
                    can_dc = Convert.ToInt32(dc.total_und);
                    can_dp = Convert.ToInt32(dp.stock);
                    if (can_dc == can_dp)
                    {
                        //elimina la compra, pero si no son iguales entonnces quiere decir que ya se ha empezado a utilizar esa compra y no se puede eliminar por que causaria un conflito
                        var prod = new producto();
                        prod = db.producto.Where(x => x.id_producto == dc.id_producto).SingleOrDefault();///seleciono el prod del detalle de compra
                        dp.estado = false;//pongo en falso el estado de detalle de producto
                        dc.estado = false;//pongo en falso el detalle de compra
                        prod.stock = prod.stock - can_dc;//resto la cantidad que se compro del stock que esta en producto

                        //db.Entry(dp).State = EntityState.Modified;
                        db.detalle_producto.Remove(dp);
                        db.detalle_compra.Remove(dc);
                        db.Entry(prod).State = EntityState.Modified;
                    }
                    else
                    {
                        comprobacion = 1;
                        break;
                    }
                }
                if (comprobacion == 0)
                {
                    db.compra.Remove(compra);
                    db.SaveChanges();
                    //guarda los cambios,,,     sino no guardes nada y manda un mensaje al usuario
                }
                else
                {
                    ViewBag.Error = "No se puedo anular la venta,por que ya se han empezado a vender";//aqui tendria que ir el viewbag para mandar el erro al usuario de que no se pudo anular esa compra
                }
            }
            else
            {
                ViewBag.Error = "No hay compra para anular";
                //no hay compra para eliminar
            }
            return RedirectToAction("index");
        }

        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Detail(int? id)
        {
            if (id != null)
            {
                CompraDetalleViewModel model = new CompraDetalleViewModel();
                model.Compra = db.compra.Include("proveedor").Include("usuario").Where(x => x.id_compra == id).SingleOrDefault();
                model.DetalleCompra = db.detalle_compra.Include("producto").Include("lote").Where(x => x.id_compra == id).ToList();
                return View(model);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public JsonResult listarProductos()
        {
            var List = (from tabla in db.vista_producto
                        where tabla.estado == true
                        select new
                        {
                            id = tabla.id_producto,
                            category = tabla.nombre_producto + " " + tabla.nombre_presentacion + " " + tabla.nombre_laboratorio + " " + tabla.codigo_producto,
                            element2 = "_" + tabla.undxcajas
                        }).OrderByDescending(x => x.id).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarProveedores()
        {
            var List = (from tabla in db.proveedor
                        where tabla.estado == true
                        select new
                        {
                            id = tabla.id_proveedor,
                            category = tabla.nombre_proveedor
                        }).OrderByDescending(x => x.id).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarLote()
        {
            var List = (from tabla in db.lote
                        where tabla.estado == true
                        select new
                        {
                            id = tabla.id_lote,
                            category = tabla.nlote + "_" + tabla.fecha_vencimiento,
                            element2 = "_" + ((DateTime)tabla.fecha_vencimiento)
                        }).OrderByDescending(x => x.id);
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarLote(lote lote)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,
                Redirect = "",
                Error = ""
            };

            if (ModelState.IsValid)
            {
                try
                {
                    lote.estado = true;
                    db.lote.Add(lote);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta = verif.VerificarLote(lote.nlote);
                    respuesta.Respuesta = false;
                    if (rpta) { respuesta.Error = "Ya existe un mismo Numero lote"; }
                    else { respuesta.Error = "Hubo un problema al guardar"; }
                }

            }
            else
            {
                respuesta.Respuesta = false;
                ModelState.AddModelError("fecha_vencimiento", "La fecha de vencimiento es obligatorio");
                respuesta.Error = "La fecha de vencimiento es obligatorio";
            }

            return Json(respuesta);
        }

        public JsonResult GuardarProveedor(proveedor proveedor)
        {
            ResponseModel Respuesta = new ResponseModel
            {
                Respuesta = true,Redirect = "",Error = "",ErrorAdicional = "",ErrorEntidad = ""
            };
            if (ModelState.IsValid)
            {
                try
                {
                    proveedor.estado = true;
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();
                    Respuesta.Respuesta = true;

                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rptanombre, rptaruc;
                    rptanombre = verif.VerificarProveedor(proveedor.nombre_proveedor);
                    if (rptanombre == true)
                    {
                        ModelState.AddModelError("errorProveedor", "Ya existe un proveedor con este nombre, agruegue uno diferente");
                        Respuesta.Error = "Ya existe un proveedor con este nombre, agruegue uno diferente";
                        Respuesta.Respuesta = false;
                    }

                    rptaruc = verif.VerificarProveedorRuc(proveedor.ruc);
                    if (rptaruc == true)
                    {
                        ModelState.AddModelError("ruc", "Ya existe un proveedor con este ruc, agruegue uno diferente");
                        Respuesta.ErrorAdicional = "Ya existe un proveedor con este ruc, agruegue uno diferente";
                        Respuesta.Respuesta = false;
                    }
                    if (rptanombre == false && rptaruc == false)
                    {
                        ModelState.AddModelError("errorProveedor", "Ocurrio un error al guadar");
                        Respuesta.ErrorEntidad = "Ocurrio un error al guadar";
                        Respuesta.Respuesta = false;
                    }
                }
            }

            return Json(Respuesta);
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