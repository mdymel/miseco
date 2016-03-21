namespace MiSeCo.Sample.Application
{
    public class Program
    {
        public interface IFirstDynamicInterface : IContractInterface
        {
        }

        public static void Main(string[] args)
        {
            var miseco = new MiSeCo();
            var service1 = miseco.CreateServiceObject<IFirstDynamicInterface>();
        }
    }
}
