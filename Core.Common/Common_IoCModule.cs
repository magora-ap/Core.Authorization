using Autofac;
using Core.Authorization.Common.Concrete.Helpers;

namespace Core.Authorization.Common
{
    public sealed class Common_IoCModule : Module
    {
        private ConfigurationSettings Settings { get; set; }
        public Common_IoCModule(ConfigurationSettings options)
        {
            Settings = options;
        }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            
        }
    }
}
