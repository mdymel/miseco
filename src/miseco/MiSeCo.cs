using System;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace MiSeCo
{
    internal class MethodInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"New call: {invocation.Method.Name}");
            Type returnType = invocation.Method.ReturnType;
            invocation.ReturnValue = returnType.IsValueType
                ? Activator.CreateInstance(returnType)
                : null;
        }
    }

    public class MiSeCo
    {
    public T CreateServiceObject<T>() where T:IContractInterface
    {
        var generator = new ProxyGenerator();
        var proxy = generator.CreateInterfaceProxyWithoutTarget(typeof(T), new MethodInterceptor());
        return (T)proxy;
    }
    }
}