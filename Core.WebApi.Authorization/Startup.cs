using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Authorization.Bll;
using Core.Authorization.Common.Concrete.Helpers;
using Core.Authorization.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.Authorization.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Core.Authorization.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //get settings and start migrations
            var config = Configuration.GetSection("Settings");
            new MigrationsRunnerDal(config.Get<ConfigurationSettings>()).MigrateToLatest();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Configure ConnectionStrings using config
            var config = Configuration.GetSection("Settings");
            services.Configure<ConfigurationSettings>(config);
            // Add framework services.
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Auth", policy => policy.Requirements.Add(new AuthRequirement()));
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });

            services.AddSingleton<IAuthorizationHandler, AuthHandler>();

            // Setup options with DI
            services.AddOptions();
            // Create the Autofac container builder.
            var builder = new ContainerBuilder();
            // Add any Autofac modules or registrations.
            builder.RegisterModule(new Dal_IoCModule(config.Get<ConfigurationSettings>()));
            builder.RegisterModule(new Bll_IoCModule(config.Get<ConfigurationSettings>()));
            // Populate the services.
            builder.Populate(services);
            // Build the container.
            this.ApplicationContainer = builder.Build();
            // Create and return the service provider.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }

    }
}
