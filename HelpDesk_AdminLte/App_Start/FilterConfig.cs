using System.Web;
using System.Web.Mvc;

namespace HelpDesk_AdminLte
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
