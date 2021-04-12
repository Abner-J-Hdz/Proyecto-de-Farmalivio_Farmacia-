using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farmalivio_Farmacia.Models;

namespace Farmalivio_Farmacia.Controllers
{
    public class InicioController : Controller
    {
        //private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();
        // GET: Inicio
        public ActionResult Index(string nameuser)
        {
            ViewBag.Name = nameuser;
            return View();
        }

        public JsonResult dtoUSer()
        {
            var UserLogin = (usuario)Session["User"];
            string nombre = UserLogin.nombre_user + " " + UserLogin.apellido_user;
            string imagen = UserLogin.imagen;
            
            //string nameUser = UserLogin.nombre_user + " " + UserLogin.apellido_user;
            return Json(nombre, JsonRequestBehavior.AllowGet);
        }
    }
}