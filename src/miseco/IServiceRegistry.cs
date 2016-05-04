namespace MiSeCo
{
    internal interface IServiceRegistry
    {
        void Enable();
        bool IsEnabled();
        ServiceInformation FindServiceByName(string serviceName);
        void RegisterService(ServiceInformation serviceInformation);
    }
}