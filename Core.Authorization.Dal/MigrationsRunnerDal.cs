using Core.Authorization.Common.Concrete.Helpers;
using Core.Authorization.Dal.Migrations;
using Core.Dal.Common;

namespace Core.Authorization.Dal
{
    public class MigrationsRunnerDal : MigrationsRunnerAbstract
    {
        public MigrationsRunnerDal(ConfigurationSettings options) : base(typeof(MigrationDalNamespace), options.ConnectionString)
        {
        }
    }
}
