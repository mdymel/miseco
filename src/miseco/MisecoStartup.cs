using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
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
            // add other framework services

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            var frames = new StackTrace().GetFrames();
            var assembly = GetServiceAssembly();

            containerBuilder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.GetInterfaces().Contains(typeof(IContractInterface)))
                   .AsImplementedInterfaces();

            //containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
