using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmalivio_Farmacia.Filter
{
    public class AuthorizeUser: AuthorizeAttribute
    {
        private usuario user;
        private Farmacia_FarmalivioEntities Contex = new Farmacia_FarmalivioEntities();
        //private int idrol;
        private string rol;
        private string catalogo;
        private rol ObtenerRol = new rol();
        private int IdRolAdmin;
        private int IdRolVendedor;
        public AuthorizeUser(string rol = "", string catalogo = "")
        {
            this.catalogo = catalogo;
            this.rol = rol;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            user = (usuario)HttpContext.Current.Session["User"];
            //string RolUserCockie = Convert.ToString(user.rol);
            //obten el id del rol que paso al filtro para ver si el usuario tiene ese rol
            //ObtenerRol = Contex.rol.Where(x => x.rol1 == rol).SingleOrDefault();
            //int idRol = ObtenerRol.id;
            try
            {
                ObtenerRol = Contex.rol.Where(x => x.rol1 == "Administrador").SingleOrDefault();
                IdRolAdmin = ObtenerRol.id_rol;

                ObtenerRol = Contex.rol.Where(x => x.rol1 == "Vendedor").SingleOrDefault();
                IdRolVendedor = ObtenerRol.id_rol;

                //si el rol que te paso al filtros administrador y ese rol te corresponde entonces..
                if (rol == "Administrador" && user.id_rol == IdRolAdmin)
                {
                    //pass to the accion
                }
                //si el rol que te paso al filtros vendedor pero tu rol es administrador..
                if (rol == "Vendedor" && user.id_rol == IdRolAdmin)
                {
                    //pass to the accion
                }
                //si el rol que te paso al filtros administrador pero tu rol es vendedor..
                if (rol == "Administrador" && user.id_rol == IdRolVendedor)
                {
                    //no pass to the accion
                    filterContext.Result = new RedirectResult("~/error/index?=Catalogo" + catalogo);
                }
                if (rol == "Vendedor" && user.id_rol == IdRolVendedor)
                {
                    //pass to the accion
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/error/index?=Catalogo" + catalogo);

            }


        }



    }
}