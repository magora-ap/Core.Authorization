using System;
using Autofac;
using Autofac.Features.AttributeFilters;
using Core.Authorization.Common.Concrete.Helpers;
using Core.Authorization.Dal.Abstract;
using Core.Authorization.Dal.Repository;
using Core.Dal.Common.Connection;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Core.Authorization.Dal
{
    /// <summary>
    ///     Data access level IoC configuration
    /// </summary>
    /// Initial author Sergey Sushenko
    public sealed class Dal_IoCModule : Module
    {
        private ConfigurationSettings Settings { get; set; }
        public Dal_IoCModule(ConfigurationSettings options)
        {
            Settings = options;
        }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(
                   x => new PostgreConnection(Settings.ConnectionString)).As<PostgreConnection>()
               .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>().As<IUserRepository>().WithAttributeFiltering();
        }
    }
}
