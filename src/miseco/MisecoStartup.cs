using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable ConsiderUsingConfigureAwait

namespace MiSeCo
{
    public abstract class MisecoStartup
    {
        public abstract Assembly GetServiceAssembly();

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            Assembly assembly = GetServiceAssembly();

            containerBuilder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.GetInterfaces().Contains(typeof(IContractInterface)))
                    .AsImplementedInterfaces();
            containerBuilder.RegisterType<ServicesRegistry>().AsImplementedInterfaces();
            containerBuilder.Populate(services);
            IContainer container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
