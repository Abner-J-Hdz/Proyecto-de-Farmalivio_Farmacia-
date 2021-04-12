using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class VentaViewModel:Paginacion
    {
        public List<vista_venta> Venta { get; set; }
        public string Criterio { get; set; }
    }
}