using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class PatologiaViewModel : Paginacion
    {
        public List<patologia> Patologias { get; set; }
    }
}