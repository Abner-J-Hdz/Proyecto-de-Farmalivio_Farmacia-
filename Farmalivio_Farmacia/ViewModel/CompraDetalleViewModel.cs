using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class CompraDetalleViewModel
    {
        public compra Compra { get; set; }
        public List<detalle_compra> DetalleCompra { get; set; }
    }
}