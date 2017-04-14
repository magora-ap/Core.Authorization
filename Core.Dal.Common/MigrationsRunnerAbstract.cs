using System.Diagnostics;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.Postgres;

namespace Core.Dal.Common
{
    public abstract class MigrationsRunnerAbstract
    {
        public abstract string Namespace { get; }
        public abstract string ConnectionString { get; }
        public abstract Assembly Assembly { get; }
        public void MigrateToLatest()
        {
            var announcer = new TextWriterAnnouncer(s => Debug.WriteLine(s));


            var migrationContext = new RunnerContext(announcer)
            {
                Namespace = Namespace
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new PostgresProcessorFactory();

            using (var processor = factory.Create(ConnectionString, announcer, options))
            {
                var runner = new MigrationRunner(Assembly, migrationContext, processor);
                runner.MigrateUp(true);
            }
        }
    }
    public class MigrationOptions : IMigrationProcessorOptions
    {
        public bool PreviewOnly { get; set; }

        public string ProviderSwitches { get; set; }

        public int Timeout { get; set; }
    }
}
