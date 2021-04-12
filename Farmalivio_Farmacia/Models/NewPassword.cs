using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.Models
{
    public class NewPassword
    {
        [MaxLength(25, ErrorMessage = "Maximo de carateres 25")]
        [MinLength(5, ErrorMessage = "Minimo de carateres 5")]
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [PasswordPropertyText]
        public string contrasena { get; set; }

        [MaxLength(25, ErrorMessage = "Maximo de carateres 25")]
        [MinLength(6, ErrorMessage = "Minimo de carateres 6")]
        [DisplayName("Nueva contraseña")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [PasswordPropertyText]
        public string newcontrasena { get; set; }

        [MaxLength(25, ErrorMessage = "Maximo de carateres 25")]
        [MinLength(6, ErrorMessage = "Minimo de carateres 6")]
        [DisplayName("Repita contraseña")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [PasswordPropertyText]
        public string newcontrasena2 { get; set; }

        public bool guardarpass(usuario usuario)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new usuario();
                varCat = contex.usuario.Where(x => x.id_usuario == usuario.id_usuario).SingleOrDefault();
                if (varCat != null)
                {
                    varCat.contrasena = usuario.contrasena;
                    contex.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
       
    }
}