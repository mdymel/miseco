using Castle.DynamicProxy;

namespace MiSeCo
{
    public class MiSeCo
    {
    public T CreateServiceObject<T>() where T:IContractInterface
    {
        var generator = new ProxyGenerator();
        object proxy = generator.CreateInterfaceProxyWithoutTarget(typeof(T), new MethodInterceptor());
        return (T)proxy;
    }
    }
}