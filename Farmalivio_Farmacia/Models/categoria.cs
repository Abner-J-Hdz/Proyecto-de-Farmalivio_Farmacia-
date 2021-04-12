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

    public partial class categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public categoria()
        {
            this.producto_categoria = new HashSet<producto_categoria>();
        }

        public int id_categoria { get; set; }

        [MaxLength(30, ErrorMessage = "Maximo de carateres 30")]
        [MinLength(2, ErrorMessage = "Minimo de carateres 2")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Categoria")]
        public string nombre_categoria { get; set; }

        [MaxLength(50, ErrorMessage = "Maximo de carateres 25")]
        [DisplayName("Descripcion")]
        public string descripcion_categoria { get; set; }

        [DisplayName("Estado")]
        public bool estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<producto_categoria> producto_categoria { get; set; }
    }
}