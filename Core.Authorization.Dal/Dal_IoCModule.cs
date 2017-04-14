using System;
using Autofac;
using Autofac.Features.AttributeFilters;
using Core.Authorization.Common.Concrete.Helpers;
using Core.Authorization.Dal.Abstract;
using Core.Authorization.Dal.Repository;
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
                    x =>
                    {
                        var connection = NpgsqlFactory.Instance.CreateConnection() as NpgsqlConnection;
                        connection.ConnectionString = Settings.ConnectionString;
                        return connection;
                    }).As<NpgsqlConnection>()
                .InstancePerLifetimeScope();
                //.OnActivated(x => { x.Instance.Open(); })
                //.OnRelease(x => { x.Close(); });

            builder.RegisterType<UserRepository>().As<IUserRepository>().WithAttributeFiltering();
        }
    }
}
