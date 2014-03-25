using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace MvcSample.Web.Filters
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public int StatusCode { get; set; }

        public override async Task Invoke(ExceptionFilterContext context)
        {
            context.ActionResult = new HttpStatusCodeResult(StatusCode);
            context.ExceptionHandled = (StatusCode != 418);
        }
    }
}