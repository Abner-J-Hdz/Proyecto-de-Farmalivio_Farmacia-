//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farmalivio_Farmacia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class vista_venta
    {
        public int id_venta { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha { get; set; }
        public decimal total { get; set; }
        public string num_fact { get; set; }
        public string usuario { get; set; }
        public string cliente { get; set; }
        public string nombre_estado { get; set; }
        public int id_usuario { get; set; }
    }
}