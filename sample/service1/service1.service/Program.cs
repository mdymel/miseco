using Microsoft.AspNet.Hosting;

namespace MiSeCo.Sample.Service1.Service
{
    public class Program
    {
        public class Startup : MisecoStartup
        {
            
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}