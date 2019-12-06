using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> exceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            exceptionQueue.Enqueue(filterContext.Exception);
            filterContext.RequestContext.HttpContext.Response.Redirect("/Views/ErrorView.cshtml");
        }
    }
}