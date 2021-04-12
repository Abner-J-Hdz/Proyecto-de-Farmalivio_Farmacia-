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

    public partial class proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proveedor()
        {
            this.compra = new HashSet<compra>();
            this.devolucion = new HashSet<devolucion>();
        }

        public int id_proveedor { get; set; }

        [MaxLength(30, ErrorMessage = "Maximo de carateres 30")]

        [DisplayName("Direccion")]

        public string direccion { get; set; }

        [DisplayName("Estado")]

        public bool estado { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Proveedor")]
        [MaxLength(25, ErrorMessage = "Maximo de carateres 25")]
        [MinLength(2, ErrorMessage = "Minimo de carateres 2")]

        public string nombre_proveedor { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Ruc")]

        public string ruc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compra> compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<devolucion> devolucion { get; set; }
    }
}
