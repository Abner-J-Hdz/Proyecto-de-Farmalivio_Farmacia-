using Farmalivio_Farmacia.Filter;
using Farmalivio_Farmacia.helpers;
using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmalivio_Farmacia.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        
        public ActionResult Index()
        {
            if (Session["User"]!=null)
            {
                return Redirect("~/inicio/index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult index(string email, string password)
        {
            if (email == null && password == null)
            {
                return Redirect("~/Login/index");
            }
            try
            {
                using (var Contex = new Farmacia_FarmalivioEntities())
                {
//                    var usuario = new usuario();
                    password = HelperHash.MD5(password);
                    var usuario = Contex.usuario.Include("rol")
                                  .Where(x => x.email == email && x.contrasena == password && x.estado==true)
                                  .FirstOrDefault();
                    if (usuario == null)
                    {
                        ViewBag.error = "La contraseña o correo no es valido";
                        return View("index");
                    }
                    Session["User"] = usuario;
                  
                }
                var UserLogin = (usuario)Session["User"];
                //Session["Nombreusuario"] = UserLogin.nombre_user + " " + UserLogin.apellido_user;
                //Session["imagen"] = UserLogin.imagen;
                //string nameUser = UserLogin.nombre_user + " " + UserLogin.apellido_user;
                return Redirect("~/inicio/index/");
            }
            catch (Exception e)
            {
                throw;
                //ViewBag.error = "La contraseña o correo no es valido";
                //return View("index");
  
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            //Session["Nombreusuario"] = null;
            //Session["imagen"] = null;
            return Redirect("~/login/index");
        }
    }
}