using System;
using MiSeCo.Sample.Service1.Contract;

namespace MiSeCo.Sample.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var miseco = new MiSeCo();
            var service1 = miseco.CreateServiceObject<IService1>();
            Console.WriteLine($"2+2={service1.Add(2, 2)}");
        }
    }
}
