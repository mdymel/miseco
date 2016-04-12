namespace MiSeCo
{
    public class InvocationApiModel
    {
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public object[] Parameters { get; set; }
    }
}