using System.Web;
using System.Web.Mvc;

namespace Farmalivio_Farmacia
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filter.VerifySession());
        }
    }
}
