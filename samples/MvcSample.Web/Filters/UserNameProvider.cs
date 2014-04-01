using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace MvcSample.Web.Filters
{
    public class UserNameProvider : IActionFilter, IOrderedFilter
    {
        private readonly string[] _userNames;
        private static int _index;

        public UserNameProvider()
            : this(new [] { "Jon", "David", "Goliath" })
        {
        }

        public UserNameProvider(string[] userNames)
        {
            _userNames = userNames;
        }

        public async Task Invoke(ActionFilterContext context, Func<Task> next)
        {
            object originalUserName = null;

            context.ActionArguments.TryGetValue("userName", out originalUserName);

            var userName = originalUserName as string;

            if (string.IsNullOrWhiteSpace(userName))
            {
                context.ActionArguments["userName"] = _userNames[(_index++) % 3];
            }

            await next();
        }

        public int Order
        {
            get;
            set;
        }
    }
}
