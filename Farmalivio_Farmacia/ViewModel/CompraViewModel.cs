using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class CompraViewModel:Paginacion
    {
        public List<vista_Compra_listado> Compras { get; set; }
    }
}