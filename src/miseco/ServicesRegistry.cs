using System.Collections.Generic;

namespace MiSeCo
{
    internal class ServicesRegistry : IServiceRegistry
    {
        private readonly Dictionary<string, ServiceInformation> _registrations;
        private bool _isEnabled;

        public ServicesRegistry()
        {
            _registrations = new Dictionary<string, ServiceInformation>();
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public bool IsEnabled()
        {
            return _isEnabled;
        }

        public ServiceInformation FindServiceByName(string serviceName)
        {
            if (!_registrations.ContainsKey(serviceName)) return null;
            return _registrations[serviceName];
        }

        public void RegisterService(ServiceInformation serviceInformation)
        {
            _registrations.Add(serviceInformation.ServiceName, serviceInformation);
        }
    }
}