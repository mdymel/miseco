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

        [HttpPost]
        [Route("invokeMethod")]
        public object Services([FromBody] InvocationApiModel model)
        {
            IContractInterface service = _services.FirstOrDefault(s => s.GetType().GetInterfaces().Any(i => i.Name == model.ServiceName));
            if (service == null) throw new Exception($"Service {model.ServiceName} could not be found");

            MethodInfo methodInfo = service.GetType().GetMethods().First(m => m.Name == model.MethodName);
            if (methodInfo == null) throw new Exception($"Method {model.MethodName} not found in service {model.ServiceName}");

            var methodParameters = methodInfo.GetParameters();
            if (methodParameters.Length == 0) methodInfo.Invoke(service, null);

            if (methodParameters.Length != model.Parameters.Length) throw new Exception($"Wrong number of parameters for method {model.ServiceName}.{model.MethodName}");
            var parameters = new List<object>();
            for (int i = 0; i < methodParameters.Length; i++)
            {
                object value = Convert.ChangeType(model.Parameters[i], methodParameters[i].ParameterType);
                parameters.Add(value);
            }

            return methodInfo.Invoke(service, parameters.ToArray());
        }
    }
}