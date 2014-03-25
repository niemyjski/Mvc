using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Filters;

namespace Microsoft.AspNet.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ExceptionFilterAttribute : Attribute, IAuthorizationFilter, IActionFilter, IActionResultFilter, IOrderedFilter
    {
        public int Order { get; set; }

        public bool IsPreambleFilter
        {
            get { return true; }
        }

        public virtual Task Invoke(AuthorizationFilterContext context, Func<Task> next)
        {
            return InvokeInternal(context, next, ex => 
            {
                context.Fail();
                return new ExceptionFilterContext(context, ex);
            });
        }

        public virtual Task Invoke(ActionFilterContext context, Func<Task> next)
        {
            return InvokeInternal(context, next, ex =>
            {
                return new ExceptionFilterContext(context, ex);
            });
        }

        public virtual Task Invoke(ActionResultFilterContext context, Func<Task> next)
        {
            return InvokeInternal(context, next, ex =>
            {
                return new ExceptionFilterContext(context, ex);
            });
        }

        public abstract Task Invoke(ExceptionFilterContext context);

        private async Task InvokeInternal<TFilterContext>(TFilterContext context, 
                                                          Func<Task> next,
                                                          Func<Exception, ExceptionFilterContext> createExceptionContext)
            where TFilterContext : FilterContext
        {
            ExceptionDispatchInfo exceptionInfo = null;
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                exceptionInfo = ExceptionDispatchInfo.Capture(ex);
            }

            if (exceptionInfo != null)
            {
                var exception = exceptionInfo.SourceException;
                var exceptionContext = createExceptionContext(exception);

                await Invoke(exceptionContext);

                if (!exceptionContext.ExceptionHandled)
                {
                    exceptionInfo.Throw();
                }
                else
                {
                    if (exceptionContext.ActionResult != null)
                    {
                        context.ActionResult = exceptionContext.ActionResult;
                    }
                }
            }
        }
    }
}
