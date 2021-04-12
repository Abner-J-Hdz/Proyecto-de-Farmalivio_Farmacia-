using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class LaboratorioViewModel : Paginacion
    {
        public List<laboratorio> Laboratorios { get; set; }
    }
}