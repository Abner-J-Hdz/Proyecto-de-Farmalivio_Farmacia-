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

    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            this.venta = new HashSet<venta>();
        }

        public int id_cliente { get; set; }

        [MaxLength(25, ErrorMessage = "Maximo de carateres 25")]
        [MinLength(2, ErrorMessage = "Minimo de carateres 2")]
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo es obligatorio")]

        public string nombre_cliente { get; set; }
        [MinLength(2, ErrorMessage = "Minimo de carateres 2")]
        [DisplayName("Apellido")]

        public string apellido_cliente { get; set; }
        [DisplayName("Direccion")]
        [MaxLength(100, ErrorMessage = "Maximo de carateres 100")]

        public string direccion { get; set; }

        public bool estado { get; set; }

        [DisplayName("Cedula")]
        [MaxLength(15, ErrorMessage = "Maximo de carateres 15")]
        public string cedula { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venta> venta { get; set; }
    }
}
