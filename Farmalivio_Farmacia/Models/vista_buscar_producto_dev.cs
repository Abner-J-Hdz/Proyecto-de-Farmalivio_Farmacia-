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
    
    public partial class vista_buscar_producto_dev
    {
        public string nombre_producto { get; set; }
        public string codigo_producto { get; set; }
        public int id_detalle_producto { get; set; }
        public int id_producto { get; set; }
        public int id_detalle_compra { get; set; }
        public int id_lote { get; set; }
        public decimal numero_caja { get; set; }
        public int undxcajas { get; set; }
        public Nullable<bool> estado { get; set; }
        public Nullable<bool> enexposicion { get; set; }
        public decimal iva { get; set; }
        public decimal precio_compra { get; set; }
        public decimal stock { get; set; }
        public int id_proveedor { get; set; }
        public System.DateTime fecha { get; set; }
        public string num_fact { get; set; }
        public decimal contiene { get; set; }
        public int id_umedida { get; set; }
        public int id_presentacion { get; set; }
        public int id_laboratorio { get; set; }
    }
}