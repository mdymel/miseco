using System;

namespace MiSeCo.Sample.Application
{
    public class Program
    {
        public interface IFirstDynamicInterface : IContractInterface
        {
            int Add(int a, int b);
            string GetString();
        }

        public static void Main(string[] args)
        {
            var miseco = new MiSeCo();
            var service1 = miseco.CreateServiceObject<IFirstDynamicInterface>();
            int c = service1.Add(2, 2);
            Console.WriteLine(c);
            string s = service1.GetString();
            Console.WriteLine($"s is null: {s == null}");
        }
    }
}
