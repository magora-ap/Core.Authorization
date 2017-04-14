using System;
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
        private string Namespace => NamespaceType.Namespace;
        private string ConnectionString { get; }
        
        private Type NamespaceType { get; set; }
        private Assembly Assembly => NamespaceType.GetTypeInfo().Assembly;

        protected MigrationsRunnerAbstract(Type namespaceType, string connectionString)
        {
            NamespaceType = namespaceType;
            ConnectionString = connectionString;
        }

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
