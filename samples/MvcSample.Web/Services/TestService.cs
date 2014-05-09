using System;

namespace MvcSample.Web.Services
{
   public interface ITestService
   {
       string GetFoo();
   }


   public class TestService : ITestService
   {
       public string GetFoo()
       {
           return "Hello world " + DateTime.UtcNow;
       }
   }
}