using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.Models
{
    public class ResponseModelCliente
    {
        public bool Respuesta { get; set; }
        public string ErrorNombre { get; set; }
        public bool ErrorApellido { get; set; }
        public bool ErrorDireccion { get; set; }
        public bool ErrorCedula { get; set; }
    }
}