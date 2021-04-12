using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.helpers
{
    public class ObtenerDatos
    {
        public compra obtenerCompra(int id) {
            compra shop = new compra();
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                shop = contex.compra.Where(x => x.id_compra == id).SingleOrDefault();
            }
            return shop;
        }

        public int  obtenerNRegistro(int id)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
               var shop = contex.detalle_compra.Where(x => x.id_compra == id).ToList();
                return shop.Count();
            }
        }

        //public List<detalle_compra> listadoDetalleCompra(int id)
        //{
        //    using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
        //    {
        //        var shop = 1;

        //    //var List = (from tabla in db.vista_producto
        //    //            where tabla.estado == true
        //    //            select new
        //    //            {
        //    //                id = tabla.id_producto,
        //    //                category = tabla.nombre_producto + " " + tabla.nombre_presentacion + " " + tabla.nombre_laboratorio + " " + tabla.codigo_producto,
        //    //                element2 = "_" + tabla.undxcajas
        //    //            }).OrderByDescending(x => x.id).ToList();
        //    //    return Json(List, JsonRequestBehavior.AllowGet);
        //        //contex.detalle_compra.Where(x => x.id_compra == id).ToList();
        //        return shop;
        //    }
        //}

    }
}