using Farmalivio_Farmacia.Controllers;
using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmalivio_Farmacia.Filter
{
    public class VerifySession : ActionFilterAttribute
    {
        private usuario user;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);
                user = (usuario)HttpContext.Current.Session["User"];
                if (user == null)
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Login/index");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Login/index");
                throw;
            }
        }
    }
}