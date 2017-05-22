using Core.Authorization.Bll;
using Core.Authorization.Bll.Helpers;
using Core.Authorization.Common.Concrete.Helpers;
using Core.Authorization.Dal;

namespace Core.Authorization.WebApi.Run
{
    public static class IoCConfig
    {

        public static void Initialize(ConfigurationSettings settings)
        {
            IoC.Initialize(
                new Bll_IoCModule(settings),
                new Dal_IoCModule(settings));
        }
    }
}
