using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc;

namespace MiSeCo
{
    public class ApiController : Controller
    {
        private readonly IEnumerable<IContractInterface> _services;

        public ApiController(IEnumerable<IContractInterface> services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("")]
        public string Services()
        {
            var service = _services.First(s => s.GetType().Name == "Service1");
            var methodInfo = service.GetType().GetMethods().First(m => m.Name == "Add");
            if (methodInfo != null)
            {
                var parameters = methodInfo.GetParameters();
                
                if (parameters.Length == 0)
                {
                    // This works fine
                    methodInfo.Invoke(service, null);
                }
                else
                {
                    object[] parametersArray = { 2, 2 };         
                    return methodInfo.Invoke(service, parametersArray).ToString();
                }
            }
            return "error";
        }
    }
}