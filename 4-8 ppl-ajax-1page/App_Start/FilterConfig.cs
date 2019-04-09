using System.Web;
using System.Web.Mvc;

namespace _4_8_ppl_ajax_1page
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
