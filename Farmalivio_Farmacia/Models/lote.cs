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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class lote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lote()
        {
            this.detalle_compra = new HashSet<detalle_compra>();
            this.detalle_producto = new HashSet<detalle_producto>();
        }

        public int id_lote { get; set; }

        [MaxLength(20, ErrorMessage = "Maximo de carateres 25")]
        [MinLength(2, ErrorMessage = "Minimo de carateres 2")]
        [DisplayName("N Lote")]
        [Required(ErrorMessage = "El campo es obligatorio")]

        public string nlote { get; set; }

        [DisplayName("Vencimiento")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]

        public System.DateTime fecha_vencimiento { get; set; }

        public bool estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_compra> detalle_compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_producto> detalle_producto { get; set; }
    }
}