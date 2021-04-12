using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmalivio_Farmacia.Filter
{
    public class NoLogin: AuthorizeAttribute
    {
        private usuario user;
        private Farmacia_FarmalivioEntities Contex = new Farmacia_FarmalivioEntities();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            user = (usuario)HttpContext.Current.Session["User"];
            //string RolUserCockie = Convert.ToString(user.rol);
            //obten el id del rol que paso al filtro para ver si el usuario tiene ese rol
            //ObtenerRol = Contex.rol.Where(x => x.rol1 == rol).SingleOrDefault();
            //int idRol = ObtenerRol.id;
            if (user!=null)
            {
                filterContext.Result = new RedirectResult("~/Inicio/Index");
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }


        }


    }
}