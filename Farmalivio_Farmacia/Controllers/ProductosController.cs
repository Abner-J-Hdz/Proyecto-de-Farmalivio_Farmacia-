using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farmalivio_Farmacia.Models;
using Farmalivio_Farmacia.ViewModel;
using System.Data.Entity.Validation;
using Farmalivio_Farmacia.helpers;
using System.Web.Routing;
using System.Net;
using System.Data.Entity;
using Farmalivio_Farmacia.Filter;

namespace Farmalivio_Farmacia.Controllers
{
    public class ProductosController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();
        // GET: Productos
        public ActionResult Index(string criterio, string busqueda, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 10;
            int totalDeRegistros = 0;
            var modelo = new ProductoViewModel();
            if (busqueda != null)
            {
                switch (criterio)
                {
                    case "Nombre":
                        var listadoNombre = db.vista_producto.Where(x => x.nombre_producto.StartsWith(busqueda)).OrderByDescending(x => x.id_producto)
                                             .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                             .Take(cantidadRegistrosPorPagina).ToList();

                        totalDeRegistros = db.vista_producto.Where(x => x.nombre_producto.StartsWith(busqueda)).Count();
                        modelo.Productos = listadoNombre;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;

                        break;

                    case "Categoria":
                        var listadoCategoria = db.vista_producto.Where(x => x.categorias.Contains(busqueda)).OrderByDescending(x => x.id_producto)
                                             .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                             .Take(cantidadRegistrosPorPagina).ToList();

                        totalDeRegistros = db.vista_producto.Where(x => x.categorias.Contains(busqueda)).Count();
                        modelo.Productos = listadoCategoria;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;

                        break;

                    case "Patologia":
                        var listadoPatologia = db.vista_producto.Where(x => x.patologias.Contains(busqueda)).OrderByDescending(x => x.id_producto)
                                             .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                             .Take(cantidadRegistrosPorPagina).ToList();

                        totalDeRegistros = db.vista_producto.Where(x => x.patologias.Contains(busqueda)).Count();
                        modelo.Productos = listadoPatologia;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;

                        break;

                    case "Laboratorio":
                        var listadoLaboratorio = db.vista_producto.Where(x => x.nombre_laboratorio.StartsWith(busqueda)).OrderByDescending(x => x.id_producto)
                                             .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                             .Take(cantidadRegistrosPorPagina).ToList();

                        totalDeRegistros = db.vista_producto.Where(x => x.nombre_laboratorio.StartsWith(busqueda)).Count();
                        modelo.Productos = listadoLaboratorio;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;
                        break;

                    case "Generico":
                        var listadoGenerico = db.vista_producto.Where(x => x.nombre_generico.StartsWith(busqueda)).OrderByDescending(x => x.id_producto)
                                             .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                             .Take(cantidadRegistrosPorPagina).ToList();

                        totalDeRegistros = db.vista_producto.Where(x => x.nombre_generico.StartsWith(busqueda)).Count();
                        modelo.Productos = listadoGenerico;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;
                        break;

                    case "Codigo":
                        var listadoCodigo = db.vista_producto.Where(x => x.codigo_producto.StartsWith(busqueda)).OrderByDescending(x => x.id_producto)
                                             .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                             .Take(cantidadRegistrosPorPagina).ToList();

                        totalDeRegistros = db.vista_producto.Where(x => x.codigo_producto.StartsWith(busqueda)).Count();
                        modelo.Productos = listadoCodigo;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;
                        break;

                    case "Presentacion":
                        var listadoPresenatcion = db.vista_producto.Where(x => x.nombre_presentacion.StartsWith(busqueda)).OrderByDescending(x => x.id_producto)
                                                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                                    .Take(cantidadRegistrosPorPagina).ToList();

                        totalDeRegistros = db.vista_producto.Where(x => x.nombre_presentacion.StartsWith(busqueda)).Count();
                        modelo.Productos = listadoPresenatcion;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;
                        break;

                    default:
                        var listadoProductos = db.vista_producto.OrderByDescending(x => x.id_producto)
                                                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                                .Take(cantidadRegistrosPorPagina).ToList();
                        totalDeRegistros = db.vista_producto.Count();
                        modelo.Productos = listadoProductos;
                        modelo.ValoresQueryString = new RouteValueDictionary();
                        modelo.ValoresQueryString["criterio"] = criterio;
                        modelo.ValoresQueryString["busqueda"] = busqueda;
                        break;
                }

                modelo.Criterio = criterio;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;

                return View(modelo);
            }
            else
            {
                var listadoProductos = db.vista_producto.OrderByDescending(x => x.id_producto)
                             .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                             .Take(cantidadRegistrosPorPagina).ToList();
                totalDeRegistros = db.vista_producto.Count();
                modelo.Productos = listadoProductos;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["criterio"] = criterio;
                modelo.ValoresQueryString["busqueda"] = busqueda;

                modelo.Criterio = criterio;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                return View(modelo);
            }
        }

        [HttpGet]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Create()
        {
            //return RedirectToAction("Index");
            ViewBag.id_generico = new SelectList(db.generico.Where(x => x.estado == true), "id_generico", "nombre_generico");
            ViewBag.id_laboratorio = new SelectList(db.laboratorio.Where(x => x.estado == true), "id_laboratorio", "nombre_laboratorio");
            ViewBag.id_presentacion = new SelectList(db.presentacion.Where(x => x.estado == true), "id_presentacion", "nombre_presentacion");
            ViewBag.id_umedida = new SelectList(db.umedida.Where(x => x.estado == true), "id_umedida", "nombre_umedida");
            return View();
        }

        public ActionResult Mover(int? id)
        {
            int id_producto;
            if (id!=null)
            {
                try
                {
                    detalle_producto det = new detalle_producto();
                    det = db.detalle_producto.Where(x => x.id_detalle_producto == id).SingleOrDefault();
                    id_producto = det.id_producto;
                    if (det.enexposicion==true)
                    {
                        det.enexposicion = false;
                    }
                    else
                    {
                        det.enexposicion = true;
                    }
                    string ID = Convert.ToString(id_producto);
                    db.SaveChanges();
                    return Redirect("~/Productos/Detail/" + det.id_producto);
                }
                catch (Exception)
                {
                    throw;
                }
               
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public JsonResult listarCategoria()
        {
            var Listcategorias = (from categoria in db.categoria
                                  where categoria.estado == true
                                  select new
                                  {
                                      id = categoria.id_categoria,
                                      category = categoria.nombre_categoria
                                  }).OrderByDescending(x => x.id).ToList();
            return Json(Listcategorias, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarPatologia()
        {
            var Listpatologias = (from patologia in db.patologia
                                  where patologia.estado == true
                                  select new
                                  {
                                      id = patologia.id_patologia,
                                      category = patologia.nombre_patologia
                                  }).OrderByDescending(x => x.id).ToList();

            return Json(Listpatologias, JsonRequestBehavior.AllowGet);
        }
        //listarLaboratorio
        public JsonResult listarLaboratorio()
        {
            var Listlaboratorios = (from laboratorio in db.laboratorio
                                  where laboratorio.estado == true
                                  select new
                                  {
                                      id = laboratorio.id_laboratorio,
                                      category = laboratorio.nombre_laboratorio
                                  }).OrderByDescending(x => x.id).ToList();

            return Json(Listlaboratorios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarGenerico()
        {
            var Listgenesricos = (from generico in db.generico
                                  where generico.estado == true
                                  select new
                                  {
                                      id = generico.id_generico,
                                      category = generico.nombre_generico
                                  }).OrderByDescending(x => x.id).ToList();

            return Json(Listgenesricos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarPresentacion()
        {
            var Listpresentacion = (from presentacion in db.presentacion
                                  where presentacion.estado == true
                                  select new
                                  {
                                      id = presentacion.id_presentacion,
                                      category = presentacion.nombre_presentacion
                                  }).OrderByDescending(x => x.id).ToList();

            return Json(Listpresentacion, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PatologiaSelecc(int? id)
        {

            if (id != null)
            {
                var ListPatSelec = (from pp in db.producto_patologia
                                    join p in db.patologia
                                    on pp.id_patologia equals p.id_patologia
                                    where pp.id_producto == id
                                    select new
                                    {
                                        id_patologia = p.id_patologia,
                                        patologia = p.nombre_patologia
                                    }).OrderByDescending(x => x.id_patologia).ToList();
                return Json(ListPatSelec, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CategoriaSelecc(int? id)
        {

            if (id != null)
            {
                var ListCatSelec = (from pc in db.producto_categoria
                                    join c in db.categoria
                                    on pc.id_categoria equals c.id_categoria
                                    where pc.id_producto == id
                                    select new
                                    {
                                        id_categoria = c.id_categoria,
                                        categoria = c.nombre_categoria
                                    }).OrderByDescending(x => x.id_categoria).ToList();
                return Json(ListCatSelec, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GuardarCategoria(categoria categoria)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,Redirect = "",Error = ""
            };

            if (ModelState.IsValid)
            {
                try
                {
                    categoria.estado = true;
                    db.categoria.Add(categoria);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta = verif.VerificarCategoria(categoria.nombre_categoria);
                    respuesta.Respuesta = false;
                    if (rpta) { respuesta.Error = "Ya existe una categoria con ese nombre"; }
                    else { respuesta.Error = "Hubo un problema al guardar"; }   
                }

            }

            return Json(respuesta);
        }
        public JsonResult GuardarPatologia(patologia patologia)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,Redirect = "",Error = ""
            };

            if (ModelState.IsValid)
            {
                try
                {
                    patologia.estado = true;
                    db.patologia.Add(patologia);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta = verif.VerificarPatologia(patologia.nombre_patologia);
                    respuesta.Respuesta = false;
                    if (rpta) { respuesta.Error = "Ya existe una patologia con ese nombre"; }
                    else { respuesta.Error = "Hubo un problema al guardar"; }
                }

            }

            return Json(respuesta);
        }
        public JsonResult GuardarPresentacion(presentacion presentacion)
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
                    presentacion.estado = true;
                    db.presentacion.Add(presentacion);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta = verif.VerificarPresentacion(presentacion.nombre_presentacion);
                    respuesta.Respuesta = false;
                    if (rpta) { respuesta.Error = "Ya existe una presentacion con ese nombre"; }
                    else { respuesta.Error = "Hubo un problema al guardar"; }
                }
            }
            return Json(respuesta);
        }
        public JsonResult GuardarLaboratorio(laboratorio laboratorio)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,Redirect = "",Error = ""
            };

            if (ModelState.IsValid)
            {
                try
                {
                    laboratorio.estado = true;
                    db.laboratorio.Add(laboratorio);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta = verif.VerificarLaboratorio(laboratorio.nombre_laboratorio);
                    respuesta.Respuesta = false;
                    if (rpta) { respuesta.Error = "Ya existe un laboratorio con ese nombre"; }
                    else { respuesta.Error = "Hubo un problema al guardar"; }
                }
            }
            return Json(respuesta);
        }
        public JsonResult GuardarGenerico(generico generico)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,Redirect = "",Error = ""
            };

            if (ModelState.IsValid)
            {
                try
                {
                    generico.estado = true;
                    db.generico.Add(generico);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rpta = verif.VerificarGenerico(generico.nombre_generico);
                    respuesta.Respuesta = false;
                    if (rpta) { respuesta.Error = "Ya existe un generico con ese nombre"; }
                    else { respuesta.Error = "Hubo un problema al guardar"; }
                }
            }
            return Json(respuesta);
        }

        [HttpPost]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public JsonResult Guardar(producto model, int[] patologias_selec = null, int[] categorias_selec = null)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,
                Redirect = "/Productos/Index/",
                Error = "",
                ErrorAdicional = "",
                ErrorEntidad = ""
            };

            if (patologias_selec != null)
            {
                //foreach (var c in patologias_selec)
                //    model.producto_patologia.Add(new producto_patologia { id = c });
            }
            else
            {
                ModelState.AddModelError("patologia", "Debe seleccionar por lo menos una patologia");
                respuesta.Respuesta = false;
                respuesta.Error = "Debe seleccionar por lo menos una patologia";
            }
            if (categorias_selec != null)
            {

            }
            else
            {
                ModelState.AddModelError("categoria", "Debe seleccionar por lo menos una categoria");
                respuesta.Respuesta = false;
                respuesta.ErrorAdicional = "Debe seleccionar por lo menos una categoria";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.estado = true;
                    model.stock = 0;
                    db.producto.Add(model);
                    foreach (var p in patologias_selec)
                    {
                        var pat = new producto_patologia { id_patologia = p };
                        db.producto_patologia.Add(pat);
                    }
                    foreach (var c in categorias_selec)
                    {
                        var cat = new producto_categoria { id_categoria = c };
                        db.producto_categoria.Add(cat);
                    }
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rptacodigo;
                    rptacodigo = verif.VerificarProductoCodigo(model.codigo_producto);
                    respuesta.Respuesta = false;
                    if (rptacodigo)
                    {
                        ModelState.AddModelError("codigo_producto", "Ya existe un producto con este codigo, agruegue uno diferente");
                        respuesta.ErrorEntidad = "Ya existe un producto con este codigo" + model.codigo_producto + ", agruegue uno diferente";
                    }
                    else
                    {
                        ModelState.AddModelError("errorProducto", "Hubo un error al guardar");
                    }
                }

            }

            return Json(respuesta);
        }

        [HttpGet]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            int ID = Convert.ToInt32(id);
            //return RedirectToAction("Index");
            ViewBag.id_generico = new SelectList(db.generico.Where(x => x.estado == true), "id_generico", "nombre_generico", producto.id_generico);
            ViewBag.id_laboratorio = new SelectList(db.laboratorio.Where(x => x.estado == true), "id_laboratorio", "nombre_laboratorio", producto.id_laboratorio);
            ViewBag.id_presentacion = new SelectList(db.presentacion.Where(x => x.estado == true), "id_presentacion", "nombre_presentacion", producto.id_presentacion);
            ViewBag.id_umedida = new SelectList(db.umedida.Where(x => x.estado == true), "id_umedida", "nombre_umedida", producto.id_umedida);

            return View(producto);
        }
        [HttpPost]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public JsonResult GuardarEdit(producto model, int[] patologias_selec = null, int[] categorias_selec = null)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,
                Redirect = "/Productos/Index/",
                Error = "",
                ErrorAdicional = "",
                ErrorEntidad = ""
            };

            if (patologias_selec == null)
            {
                ModelState.AddModelError("patologia", "Debe seleccionar por lo menos una patologia");
                respuesta.Respuesta = false;
                respuesta.Error = "Debe seleccionar por lo menos una patologia";
            }

            if (categorias_selec == null)
            {
                ModelState.AddModelError("categoria", "Debe seleccionar por lo menos una categoria");
                respuesta.Respuesta = false;
                respuesta.ErrorAdicional = "Debe seleccionar por lo menos una categoria";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var obtener = new producto();
                    obtener = db.producto.Where(x => x.id_producto == model.id_producto).SingleOrDefault();

                    obtener.nombre_producto = model.nombre_producto;
                    obtener.descripcion_producto = model.descripcion_producto;
                    obtener.codigo_producto = model.codigo_producto;
                    obtener.contiene = model.contiene;
                    obtener.precio_venta = model.precio_venta;
                    obtener.undxcajas = model.undxcajas;

                    obtener.id_generico = model.id_generico;
                    obtener.id_laboratorio = model.id_laboratorio;
                    obtener.id_presentacion = model.id_presentacion;
                    obtener.id_umedida = model.id_umedida;

                    //db.Entry(model).State = EntityState.Modified;
                    //Eliminar antiguas patologias y categorias 
                    var obtenerCat = db.producto_categoria.Where(x => x.id_producto == model.id_producto).ToList();
                    var obtenerPat = db.producto_patologia.Where(x => x.id_producto == model.id_producto).ToList();
                    for (int i = 0; i < obtenerCat.Count; i++)
                    {
                        producto_categoria categoria_P = db.producto_categoria.Find(obtenerCat[i].id_productocategoria);
                        db.producto_categoria.Remove(categoria_P);
                    }
                    for (int i = 0; i < obtenerPat.Count; i++)
                    {
                        producto_patologia patologia_P = db.producto_patologia.Find(obtenerPat[i].id_productopatologia);
                        db.producto_patologia.Remove(patologia_P);
                    }
                    ///////Agregar las nuevas patologias y categorias
                    foreach (var p in patologias_selec)
                    {
                        var pat = new producto_patologia { id_patologia = p, id_producto = model.id_producto };
                        db.producto_patologia.Add(pat);
                    }
                    foreach (var c in categorias_selec)
                    {
                        var cat = new producto_categoria { id_categoria = c, id_producto = model.id_producto };
                        db.producto_categoria.Add(cat);
                    }
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    VerificacionDuplicado verif = new VerificacionDuplicado();
                    bool rptacodigo;
                    rptacodigo = verif.VerificarProductoCodigoEdit(model.codigo_producto, model.id_producto);
                    if (rptacodigo)
                    {
                        ModelState.AddModelError("codigo_producto", "Ya existe un producto con este codigo, agruegue uno diferente");
                        respuesta.ErrorEntidad = "Ya existe un producto con este codigo" + model.codigo_producto + ", agruegue uno diferente";
                    }
                    else
                    {
                        ModelState.AddModelError("errorProducto", "Hubo un error al guardar");
                        respuesta.ErrorEntidad = "Ocurrio un error al Actualizar";
                    }
                    respuesta.Respuesta = false;
                    //throw;
                }
            }
            else
            {
                respuesta.Respuesta = false;
            }


            return Json(respuesta);
        }

        [AuthorizeUser(rol: "Vendedor", catalogo: "Generico")]
        public ActionResult Detail(int? id)
        {
            ProductoDetailViewModel model = new ProductoDetailViewModel();

            model.Producto = db.vista_producto.Where(x => x.id_producto == id).SingleOrDefault();
            model.Detalle_Producto = db.detalle_producto.Where(x => x.id_producto == id && x.stock!=0).ToList();

            return View(model);
        }

        [AuthorizeUser(rol: "Vendedor", catalogo: "Generico")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new producto();
            try
            {
                obtener = db.producto.Where(x => x.id_producto == ID).SingleOrDefault();
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
                //return RedirectToAction("Index");
                //throw;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult BajarCantidad(int? id_detalle_producto)
        {
            //ViewBag.id_generico = new SelectList(db.generico.Where(x => x.estado == true), "id_generico", "nombre_generico");
            ViewBag.id_motivo = new SelectList(db.motivo, "id_motivo", "nombre_motivo");
            return View();
        }

        [HttpPost]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public JsonResult BajarCantidad(bajar_producto bajar)
        {
            var respuesta = new ResponseModel
            {
                Respuesta = true,
                Redirect = "",
                Error = "",
                ErrorAdicional = "",
                ErrorEntidad = ""
            };
            int detalle_de_producto, detalle_stock;
            if (ModelState.IsValid==true)
            {
                //si es valido hacemos lo siguiente
                try
                {
                    if (bajar.cantidad>0)
                    {

                    var obDetalleProd = new detalle_producto();
                    obDetalleProd = db.detalle_producto.Where(x => x.id_detalle_producto == bajar.id_detalle_producto).SingleOrDefault();

                    var UserLogin = (usuario)Session["User"];
                    string rol = Convert.ToString(UserLogin.rol.rol1);
                    int idUser = Convert.ToInt32(UserLogin.id_usuario);

                    detalle_de_producto = obDetalleProd.id_detalle_producto;
                    detalle_stock = Convert.ToInt32(obDetalleProd.stock);

                        //baja la cantidad de la tabla de producto
                        var obtenerProd = new producto();
                        obtenerProd = db.producto.Where(x => x.id_producto == obDetalleProd.id_producto).SingleOrDefault();
                        obtenerProd.stock = obtenerProd.stock - bajar.cantidad;
                        if (detalle_stock >= bajar.cantidad)
                        {
                            //bajemos el stock de ese detalle
                            obDetalleProd.stock = obDetalleProd.stock - Convert.ToDecimal(bajar.cantidad);                       

                        // var modelo = new bajar_producto();
                        bajar.fecha = DateTime.Today;
                        bajar.id_usuario = idUser;

                        //añadimos a la base de datos todos los registro que vienen del objeto bajar
                        db.bajar_producto.Add(bajar);

                        db.SaveChanges();
                    }
                    else
                    {
                        //denegamos 
                        respuesta.Respuesta = false;
                        respuesta.Error = "La cantidad es mayor al stock del detalle que selecciono";
                    }
                   }
                    else
                    {
                        respuesta.Respuesta = false;
                        respuesta.Error = "La cantidad no puede ser 0";
                    }
                }
                //catch (Exception)
                //{
                //    respuesta.Respuesta = false;
                //    respuesta.Error = "Ocurrio un erro al guardar";

                //    throw;
                //}
                catch (DbEntityValidationException ex)
                {  
                    List<string> errorMessages = new List<string>();
                    //((System.Data.Entity.Validation.DbEntityValidationException)$dbEx).EntityValidationErrors;

                    foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }
                    respuesta.Respuesta = false;
                    respuesta.ListaError = errorMessages;
                    

                }
            }
            else
            {
                respuesta.Respuesta = false;
                respuesta.Error = "Ocurrio un error al guardar, por favor verifique los datos";
            }

            return Json(respuesta);
        }
        [HttpGet]
        public JsonResult listarMotivo()
        {
            var ListMotivos = (from motivo in db.motivo
                                  select new
                                  {
                                      id = motivo.id_motivo,
                                      category = motivo.nombre_motivo
                                  }).OrderByDescending(x => x.id).ToList();
            return Json(ListMotivos, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult ObtenerIdProd(int id)
        {
            var detalle = new detalle_producto();
            detalle = db.detalle_producto.Where(x => x.id_detalle_producto == id).SingleOrDefault();
            int IdProducto = detalle.id_producto;

            return Json(IdProducto, JsonRequestBehavior.AllowGet);
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