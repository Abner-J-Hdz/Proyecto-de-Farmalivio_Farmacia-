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
    
    public partial class table_test
    {
        public int id_producto { get; set; }
        public string nombre_producto { get; set; }
        public decimal contiene { get; set; }
        public string nombre_umedida { get; set; }
        public string nombre_laboratorio { get; set; }
        public string nombre_presentacion { get; set; }
        public System.DateTime fecha { get; set; }
        public int cantidad { get; set; }
        public decimal precio_venta { get; set; }
        public Nullable<decimal> totalv { get; set; }
    }
}
