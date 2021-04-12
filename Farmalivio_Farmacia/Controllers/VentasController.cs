using Farmalivio_Farmacia.Filter;
using Farmalivio_Farmacia.Models;
using Farmalivio_Farmacia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Farmalivio_Farmacia.Controllers
{
    public class VentasController : Controller
    {
        Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Ventas
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index(string criterio, string busqueda, int pagina = 1)
        {
            var UserLogin = (usuario)Session["User"];
            string rol = Convert.ToString(UserLogin.rol.rol1);
            int idUser = Convert.ToInt32(UserLogin.id_usuario);
            var cantidadRegistrosPorPagina = 10;
            var modelo = new VentaViewModel();
            if (rol == "Administrador")
            {
                if (busqueda != null)
                {
                    var Sales = db.vista_venta.Where(x => x.cliente.StartsWith(busqueda)).OrderByDescending(x => x.id_venta).
                                        Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                        Take(cantidadRegistrosPorPagina).ToList();

                    var totalDeRegistros = db.vista_venta.Where(x => x.cliente.StartsWith(busqueda)).Count();

                    modelo.Venta = Sales;
                    modelo.PaginaActual = pagina;
                    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                    modelo.TotalDeRegistros = totalDeRegistros;
                    modelo.ValoresQueryString = new RouteValueDictionary();
                    modelo.ValoresQueryString["busqueda"] = busqueda;
                    modelo.Criterio = criterio;
                    return View(modelo);
                }
                else
                {
                    var Shops = db.vista_venta.OrderByDescending(x=>x.id_venta).
                                        Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                        Take(cantidadRegistrosPorPagina).ToList();
                    var totalDeRegistros = db.venta.Include("cliente").Include("usuario").Count();
                    modelo.Venta = Shops;
                    modelo.PaginaActual = pagina;
                    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                    modelo.TotalDeRegistros = totalDeRegistros;
                    modelo.ValoresQueryString = new RouteValueDictionary();
                    modelo.ValoresQueryString[""] = busqueda;
                    modelo.Criterio = criterio;
                    return View(modelo);
                }
            }
            else
            {
                if (busqueda != null)
                {
                    var Sales = db.vista_venta.Where(x => x.cliente.StartsWith(busqueda) && x.id_usuario == idUser).OrderByDescending(x => x.id_venta).
                                        Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                        Take(cantidadRegistrosPorPagina).ToList();

                    var totalDeRegistros = db.vista_venta.Where(x => x.cliente.StartsWith(busqueda) && x.id_usuario == idUser).Count();

                    modelo.Venta = Sales;
                    modelo.PaginaActual = pagina;
                    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                    modelo.TotalDeRegistros = totalDeRegistros;
                    modelo.ValoresQueryString = new RouteValueDictionary();
                    modelo.ValoresQueryString["busqueda"] = busqueda;
                    modelo.Criterio = criterio;
                    return View(modelo);
                }
                else
                {
                    var Shops = db.vista_venta.Where(x => x.id_usuario == idUser).OrderByDescending(x => x.id_venta).
                                        Skip((pagina - 1) * cantidadRegistrosPorPagina).
                                        Take(cantidadRegistrosPorPagina).ToList();
                    var totalDeRegistros = db.vista_venta.Where(x => x.id_usuario == idUser).Count();
                    modelo.Venta = Shops;
                    modelo.PaginaActual = pagina;
                    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                    modelo.TotalDeRegistros = totalDeRegistros;
                    modelo.ValoresQueryString = new RouteValueDictionary();
                    modelo.ValoresQueryString[""] = busqueda;
                    modelo.Criterio = criterio;
                    return View(modelo);
                }
            }

        }

        [HttpGet]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public JsonResult Create(int id_cliente, decimal subtotal, detalle_venta[] detalle)
        {
            ResponseModel result = new ResponseModel();
            result.Respuesta = false;
            detalle_producto detProducto = new detalle_producto();
            //string result = "Error! No se pudo guardar!";
            if (id_cliente != 0 && id_cliente >= 0 && subtotal != 0)
            {
                DateTime fecha_ = DateTime.Now;
                var UserLogin = (usuario)Session["User"];
                int idUser = Convert.ToInt32(UserLogin.id_usuario);
                try
                {
                    var SaveVenta = new venta
                    {
                        id_usuario = idUser,
                        fecha = fecha_,
                        total = subtotal,
                        iva = 0,
                        descuento = 0,
                        id_cliente = id_cliente,
                        id_estado = 3,
                    };
                    db.venta.Add(SaveVenta);

                    foreach (var item in detalle)
                    {
                        var Detalle = new detalle_venta
                        {
                            id_producto = item.id_producto,
                            cantidad = item.cantidad,
                            precio = item.precio,
                            subtotal = item.cantidad * item.precio,
                            iva = 0,
                            descuento = 0
                        };
                        db.detalle_venta.Add(Detalle);
                        //debemos de bajar el stockdel detalle de producto correspondiente
                        //detProducto = db.detalle_producto.Where(x => x.id_producto == item.id_producto && x.estado==true && x.id_detalle_producto.min).SingleOrDefault();
                        var detProd = (from table in db.detalle_producto
                                    where table.id_producto == item.id_producto && table.stock!=0
                                    select new
                                    {
                                        id_detalle_producto = table.id_detalle_producto//ya obtuvimos los de detalle con stock y activos, para luegos restarlos
                                    }).ToArray();
                        int  stockAct,comprobar=0, reestante;
                        int CanTemp=item.cantidad;
                        ///vamos a ver cuanto tiene el primer registro con stock activo
                        ///var dp1= db.detalle_producto.Where(x=>x.id_detalle_producto==detProd[0]).
                        foreach (var d in detProd)
                        {
                            var detalle_productoreestar = db.detalle_producto.Where(x=>x.id_detalle_producto==d.id_detalle_producto).SingleOrDefault();
                            stockAct = Convert.ToInt32(detalle_productoreestar.stock);
                            if (CanTemp==0)
                            {
                                comprobar = 1;
                                break;
                            }
                            else
                            {//segui iterando
                                if (CanTemp <= stockAct)
                                {
                                    detalle_productoreestar.stock = detalle_productoreestar.stock - CanTemp;
                                    CanTemp = CanTemp - CanTemp;
                                }
                                else//quiere decir que la cantidad es mayor, para que luego itere al siguiente registro de detalleproducto
                                {
                                    CanTemp = CanTemp - stockAct;
                                    detalle_productoreestar.stock = 0;
                                }           
                            }
                        }
                    }
                    db.SaveChanges();
                    result.Respuesta = true;
                    result.Error = "Venta guardada con exito!";
                }
                catch (Exception)
                {
                    throw;
                    //result.Error = "La Venta no se pudo guardar.";
                }
            }
            else
            {
                result.Respuesta = false;
                result.Error = "No se pudo guardar la venta";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Detail(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                VentaDetalleViewModel model = new VentaDetalleViewModel();
                var UserLogin = (usuario)Session["User"];
                string rol = Convert.ToString(UserLogin.rol.rol1);
                int idUser = Convert.ToInt32(UserLogin.id_usuario);
                if (rol == "Administrador")
                {
                    model.Venta = db.vista_venta.Where(x => x.id_venta == id).SingleOrDefault();
                    //db.vista_detalle_venta.Include("proveedor").Include("usuario").Where(x => x.id_compra == id).SingleOrDefault();
                    model.Detalle_Venta = db.vista_detalle_venta.Where(x => x.id_venta == id).ToList();
                    if (model.Venta==null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    return View(model);
                }
                else
                {
                    model.Venta = db.vista_venta.Where(x => x.id_venta == id && x.id_usuario == idUser).SingleOrDefault();
                    //db.vista_detalle_venta.Include("proveedor").Include("usuario").Where(x => x.id_compra == id).SingleOrDefault();
                    model.Detalle_Venta = db.vista_detalle_venta.Where(x => x.id_venta == id).ToList();
                    if (model.Venta==null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    return View(model);
                }

            }
            //else
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
        }

        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Anular(int? id)
        {
            ViewBag.Error = "";
            if (id!=null)
            {
                venta Venta = db.venta.Find(id);
                if (Venta!=null)
                {
                    try
                    {
                        db.venta.Remove(Venta);
                        //primero debemos selecionar todos los detalles de compra para luego sacar el producto a reestaurar

                        var sale = (from detalle in db.detalle_venta
                                    where detalle.id_venta == id
                                    select new
                                    {
                                        id_detalle = detalle.id_detalle_venta
                                    }).ToArray();
                        //instancias a las clases de prodcuto detalle venta y detalle producto que es quien nos interesa
                        detalle_venta dv = new detalle_venta();
                        detalle_producto dp = new detalle_producto();

                        foreach (var item in sale)
                        {
                            dv = db.detalle_venta.Where(x => x.id_detalle_venta == item.id_detalle).SingleOrDefault();
                            //dp = db.detalle_producto.Where(x => x.id_producto == dv.id_producto).SingleOrDefault();
                            var detProd = (from table in db.detalle_producto
                                           where table.id_producto == dv.id_producto && table.stock<(table.numero_caja * table.undxcajas)
                                           select new
                                           {
                                               id_detalle_producto = table.id_detalle_producto//ya obtuvimos los de detalle con stock y activos, para luegos restarlos
                                           }).OrderByDescending(x=>x.id_detalle_producto).ToArray();

                            int cantTemp = dv.cantidad;int stockAct, diferencia;
                            foreach (var d in detProd)
                            {
                                var detalle_productoreestar = db.detalle_producto.Where(x => x.id_detalle_producto == d.id_detalle_producto).SingleOrDefault();
                                stockAct = Convert.ToInt32(detalle_productoreestar.stock);
                                int stockLimit = Convert.ToInt32(detalle_productoreestar.numero_caja) * Convert.ToInt32(detalle_productoreestar.undxcajas);
                                diferencia = stockLimit - stockAct;
                                if (cantTemp==0)
                                {
                                    break;
                                }
                                else
                                {
                                    if (cantTemp <= diferencia)
                                    {
                                        detalle_productoreestar.stock = detalle_productoreestar.stock + cantTemp;
                                        cantTemp = 0;
                                        //quiere decir de lo la cantidad alcanza en ese registro
                                        //if (diferencia!=0)//por que si diferencia es igual a 0 quiere decir que ese registro ya esta a tope
                                        //{
                                        //}
                                        
                                    }
                                    else
                                    {
                                        detalle_productoreestar.stock = detalle_productoreestar.stock + diferencia;
                                        cantTemp = cantTemp - diferencia;
                                    }
                                }

                            }
                        }
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Error = "No se pudo eliminar la venta";
                        throw;
                    }
                }
                else
                {
                    //no se encontro la venta para elimnar
                }

            }
            else
            {
                //no se encontro la venta
            }

            return RedirectToAction("index");
        }

        public JsonResult listarProductos()
        {
            var List = (from tabla in db.vista_producto
                        where tabla.estado == true
                        select new
                        {
                            id = tabla.id_producto,
                            category = tabla.nombre_producto + " " + tabla.nombre_presentacion + " " + tabla.nombre_laboratorio + " " + tabla.codigo_producto,
                            element2 = "_" + tabla.precio_venta + "_" + tabla.stock
                        }).OrderByDescending(x => x.id).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public JsonResult comprobarstock(int cantidad, int stock)
        {
            ResponseModel Respuesta = new ResponseModel
            {
                Respuesta = false,
                Error = "",
                ErrorAdicional = "",
                ErrorEntidad = ""
            };
            if (cantidad <= stock)
            {
                Respuesta.Respuesta = true;
            }
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarClientes()
        {
            var List = (from tabla in db.cliente
                        where tabla.estado == true
                        select new
                        {
                            id = tabla.id_cliente,
                            category = tabla.nombre_cliente + " " + tabla.apellido_cliente + " " + tabla.cedula
                        }).OrderByDescending(x => x.id).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
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