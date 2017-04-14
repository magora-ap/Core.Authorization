using System;
using FluentMigrator;

namespace Core.Authorization.Dal.Migrations
{
    [Migration(20170415015000)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            this.Execute.EmbeddedScript("BaselineUp.sql");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
