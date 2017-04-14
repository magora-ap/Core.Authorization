using System.Reflection;
using Core.Authorization.Common.Concrete.Helpers;
using Core.Authorization.Dal.Migrations;
using Core.Dal.Common;

namespace Core.Authorization.Dal
{
    public class MigrationsRunnerDal : MigrationsRunnerAbstract
    {
        public override string Namespace => typeof(MigrationDalNamespace).Namespace;
        public override string ConnectionString { get; }
        public override Assembly Assembly => typeof(MigrationDalNamespace).GetTypeInfo().Assembly;

        public MigrationsRunnerDal(ConfigurationSettings options)
        {
            ConnectionString = options.ConnectionString;
        }
    }
}
