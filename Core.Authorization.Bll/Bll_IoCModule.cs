using Autofac;
using Core.Authorization.Bll.Abstract;
using Core.Authorization.Bll.Helpers;
using Core.Authorization.Bll.Helpers.Cache;
using Core.Authorization.Common.Abstract;
using Core.Authorization.Common.Concrete.Helpers;

namespace Core.Authorization.Bll
{
    public sealed class Bll_IoCModule : Module
    {
        private ConfigurationSettings Settings { get; set; }

        public Bll_IoCModule(ConfigurationSettings options)
        {
            Settings = options;
        }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<TokenHelper>().As<ITokenHelper>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(RedisCacheStore<>)).As(typeof(ICacheStoreHelper<>)).SingleInstance();
            builder.RegisterType<AuthHelper>().As<IAuthHelper>().InstancePerLifetimeScope();
            builder.RegisterType<HashCryptographyHelper>().As<IHashCryptographyHelper>().InstancePerLifetimeScope();
        }
    }
}
