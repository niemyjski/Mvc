using Microsoft.AspNet.Mvc;
using MvcSample.Web.Filters;
using MvcSample.Web.Models;

namespace MvcSample.Web
{
    [ServiceFilter(typeof(PassThroughAttribute), Order = 1)]
    [ServiceFilter(typeof(PassThroughAttribute))]
    [PassThrough(Order = 0)]
    [PassThrough(Order = 2)]
    [InspectResultPage]
    [BlockAnonymous]
    public class FiltersController : Controller
    {
        private readonly User _user = new User() { Name = "User Name", Address = "Home Address" };

        [ServiceFilter(typeof(PassThroughAttribute))]
        [AllowAnonymous]
        [AgeEnhancer]
        [TypeFilter(typeof(UserNameProvider),
            parameters = new object[] { new [] { "Julious", "Homerous", "Julian" } })]
        public IActionResult Index(int age, string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                _user.Name = userName;
            }

            _user.Age = age;

            return View("MyView", _user);
        }

        [AllowAnonymous]
        [AgeEnhancer]
        [UserNameFilter(new string[] { "Foo", "Bar", "Baz" })]
        public IActionResult Index2(int age, string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                _user.Name = userName;
            }

            _user.Age = age;

            return View("MyView", _user);
        }

        public IActionResult Blocked(int age, string userName)
        {
            return Index(age, userName);
        }
    }
}