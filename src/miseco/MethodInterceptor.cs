using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Castle.Core.Interceptor;
using Newtonsoft.Json;

namespace MiSeCo
{
    internal class MethodInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.ReflectedType == null) throw new Exception("Unknown service type");
            var model = new InvocationApiModel
            {
                ServiceName = invocation.Method.ReflectedType.Name,
                MethodName = invocation.Method.Name,
                Parameters = invocation.Arguments
            };
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("invokeMethod", model)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                if (!response.IsSuccessStatusCode) return;

                string result = response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
                object returnValue = JsonConvert.DeserializeObject(result, invocation.Method.ReturnType);
                invocation.ReturnValue = returnValue;
            }
        }
    }
}