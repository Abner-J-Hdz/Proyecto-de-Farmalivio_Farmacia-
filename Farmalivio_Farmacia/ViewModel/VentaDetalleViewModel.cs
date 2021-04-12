using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class VentaDetalleViewModel
    {
        public vista_venta Venta { get; set; }
        public List<vista_detalle_venta> Detalle_Venta { get; set; }
    }
}