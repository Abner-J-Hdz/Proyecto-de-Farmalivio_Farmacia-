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

    public partial class vista_detalle_producto
    {
        public string nombre_producto { get; set; }
        public decimal numero_caja { get; set; }
        public int undxcajas { get; set; }
        public decimal stock { get; set; }
        public Nullable<bool> enexposicion { get; set; }
        public Nullable<bool> estado { get; set; }
        public decimal precio_compra { get; set; }
        public decimal iva { get; set; }
        public string codigo_producto { get; set; }
        public string nlote { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha_vencimiento { get; set; }
        public string nombre_proveedor { get; set; }
        public System.DateTime fecha { get; set; }
        public string num_fact { get; set; }
        public Nullable<decimal> descuento { get; set; }
    }
}
