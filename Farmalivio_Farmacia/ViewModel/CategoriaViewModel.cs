using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Farmalivio_Farmacia.Models;

namespace Farmalivio_Farmacia.ViewModel
{
    public class CategoriaViewModel:Paginacion
    {
        public List<categoria> Categorias { get; set; }
    }
}