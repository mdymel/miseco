using System.Reflection;
using Microsoft.AspNet.Hosting;

namespace MiSeCo.Sample.Service2.Service
{
    public class Program
    {
        public class Startup : MisecoStartup
        {
            public override Assembly GetServiceAssembly()
            {
                return Assembly.GetExecutingAssembly();
            }
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}