using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class ProdDevolverViewModel: Paginacion
    {
        public List<vista_prod_devolver> ProductosDevolver { get; set; }

    }
}