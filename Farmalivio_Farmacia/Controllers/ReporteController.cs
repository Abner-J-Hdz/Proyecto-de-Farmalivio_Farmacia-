using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Farmalivio_Farmacia.Controllers
{
    public class ReporteController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        public void Vencerse()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptProductos_Vencerse.rpt")));
                rd.SetDataSource(db.vista_productos_a_vencerse.OrderBy(x=>x.fecha_vencimiento).Take(100).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Productos a vencerse" + DateTime.Today);
            }
            catch (Exception ex)
            {

                //throw;
                Response.Write(ex.ToString());
            }
        }

        public void ProductosAgotados()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptProductos_Acabados.rpt")));
                rd.SetDataSource(db.vista_productos_Agotados.OrderBy(x => x.nombre_producto).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Productos Agotados" + DateTime.Today);
            }
            catch (Exception ex)
            {

                //throw;
                Response.Write(ex.ToString());
            }
        }

        public void ProductosA_Agotarse()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptProductos_A_Agotarse.rpt")));
                rd.SetDataSource(db.vista_proximo_a_Agotarse.OrderByDescending(x => x.stock).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Productos A Agotarse" + DateTime.Today);
            }
            catch (Exception ex)
            {

                //throw;
                Response.Write(ex.ToString());
            }
        }

        public void Productos_mas_Vendido()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptProducto_mas_Vendido.rpt")));
                rd.SetDataSource(db.view_rpt_ultimasVentas.OrderByDescending(x=>x.cantidad_vendida).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Productos mas vendidos" + DateTime.Today);
            }
            catch (Exception ex)
            {
                //throw;rptProdMasVen.rpt
                Response.Write(ex.ToString());
            }
        }

        public void Venta_de_Productos()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rpt_ultimos_Prod_Vendidos.rpt")));
                rd.SetDataSource(db.view_rpt_ultimasVentas.OrderByDescending(x => x.cantidad_vendida).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Productos vendidos los ultimos 14 dias" + DateTime.Today);
            }
            catch (Exception ex)
            {
                //throw;rptProdMasVen.rpt
                Response.Write(ex.ToString());
            }
        }
        //Rpt_de_ultimas_ventas.rpt

        public void Ultimas_Ventas()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/Rpt_de_ultimas_ventas.rpt")));
                rd.SetDataSource(db.view_rpt_ventas.OrderByDescending(x => x.fecha).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Ventas de los ultimos 14 dia" + DateTime.Today);
            }
            catch (Exception ex)
            {
                //throw;rptProdMasVen.rpt
                Response.Write(ex.ToString());
            }
        }


        public ActionResult verprod()
        {
            return View(db.view_rpt_prodmasVendido.OrderByDescending(x=>x.cantidad_vendida). ToList());
        }

        public ActionResult verventas()
        {
            return View(db.view_rpt_ultimasVentas.OrderByDescending(x=>x.cantidad_vendida).ToList());
        }

        public ActionResult verventasgeneral()
        {
            return View(db.view_rpt_ventas.ToList());
        }

    }
}