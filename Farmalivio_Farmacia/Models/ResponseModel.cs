using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.Models
{
    public class ResponseModel
    {
        public bool Respuesta { get; set; }
        public string Redirect { get; set; }
        public string Error { get; set; }
        public string ErrorAdicional { get; set; }
        public string ErrorEntidad { get; set; }
        public List<string> ListaError { get; set; }
    }
}