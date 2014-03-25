using System;
using Microsoft.AspNet.Mvc.Filters;

namespace Microsoft.AspNet.Mvc
{
    public class ExceptionFilterContext : FilterContext
    {
        public ExceptionFilterContext([NotNull] AuthorizationFilterContext context, 
                                      [NotNull] Exception exception) 
            : base(context.ActionContext, context.FilterItems)
        {
            AuthorizationFilterContext = context;
            Exception = exception;
        }

        public ExceptionFilterContext([NotNull] ActionFilterContext context,
                                      [NotNull] Exception exception)
            : base(context.ActionContext, context.FilterItems)
        {
            ActionFilterContext = context;
            Exception = exception;
        }

        public ExceptionFilterContext([NotNull] ActionResultFilterContext context,
                                      [NotNull] Exception exception)
            : base(context.ActionContext, context.FilterItems)
        {
            ActionResultFilterContext = context;
            Exception = exception;
        }

        public AuthorizationFilterContext AuthorizationFilterContext { get; private set; }

        public ActionFilterContext ActionFilterContext { get; private set; }

        public ActionResultFilterContext ActionResultFilterContext { get; private set; }

        public Exception Exception { get; private set; }

        public bool ExceptionHandled { get; set; }
    }
}
