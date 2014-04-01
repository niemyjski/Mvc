using System;
using Microsoft.AspNet.Mvc;

namespace MvcSample.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserNameFilterAttribute : TypeFilterAttribute
    {
        public UserNameFilterAttribute(string[] userNames)
            : base(typeof(UserNameProvider))
        {
            parameters = new object[] { userNames };
        }
    }
}