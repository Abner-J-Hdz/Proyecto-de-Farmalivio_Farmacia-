using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmalivio_Farmacia.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string Catalogo)
        {
            ViewBag.Catalogo = Catalogo;
            return View();
        }
    }
}