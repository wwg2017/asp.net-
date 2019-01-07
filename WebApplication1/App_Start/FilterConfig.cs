using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //如果局部就不需要这个如果不是局部 就需要放开
            //filters.Add(new MyExceptionAttribute());
        }
    }
}
