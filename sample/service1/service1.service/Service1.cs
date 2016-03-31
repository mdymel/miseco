using MiSeCo.Sample.Service1.Contract;

namespace MiSeCo.Sample.Service1.Service
{
    public class Service1 : IService1
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}