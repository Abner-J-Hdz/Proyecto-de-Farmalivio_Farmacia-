using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class ProductoDetailViewModel
    {
        public vista_producto Producto { get; set; }
        public List<detalle_producto> Detalle_Producto { get; set; }
    }
}