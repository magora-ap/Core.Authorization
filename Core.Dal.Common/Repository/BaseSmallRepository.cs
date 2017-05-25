using System;
using System.Collections.Generic;
using Core.Dal.Common.Abstract;
using Core.Dal.Common.Models;
using Core.Dal.Common.Connection;
using Core.Dal.Common.DataTable;
using Npgsql;

namespace Core.Dal.Common.Repository
{
    public abstract class BaseSmallRepository : ITransactionRepository
    {
        protected readonly PostgreConnection Connection;

        protected BaseSmallRepository(PostgreConnection connection)
        {
            Connection = connection;
        }

        public PostgreTransaction BeginTransaction()
        {

            return Connection.BeginTransaction();
        }

        protected IEnumerable<Dictionary<string, object>> RunCommand(string command)
        {
            var dt = new DataTableCore();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = Connection.ConnectionDb;
                cmd.CommandText = command;
                Connection.Open();
                dt.Load(cmd.ExecuteReader());
                Connection.Release();
            }
            return dt.Rows;
        }

        protected string GetPaginationCommand(string command, PaginationSettings paginationSettings)
        {
            if (!string.IsNullOrEmpty(paginationSettings.Where))
                command += $" WHERE {paginationSettings.Where}";
            command += $" ORDER BY {paginationSettings.OrderColumn.FullName}";
            if (paginationSettings.OrderColumn.IsDesc)
                command += $" DESC";
            command += $" LIMIT {paginationSettings.Limit} OFFSET {paginationSettings.Offset}";
            return command;
        }



        protected string GetPaginationByLastIdCommand(string command, string fromTableName, Guid? id,
            PaginationSettings paginationSettings)
        {
            if (id.HasValue)
            {
                command += $" WHERE (({paginationSettings.OrderColumn.FullName}) ";
                if (paginationSettings.OrderColumn.IsDesc)
                {
                    command += "<";
                }
                else
                {
                    command += ">";
                }
                command +=
                    $" (SELECT ({paginationSettings.OrderColumn.FullName}) FROM {fromTableName} WHERE id='{id}'))";

                if (!string.IsNullOrEmpty(paginationSettings.Where))
                    command += $" AND ({paginationSettings.Where})";
            }
            else
            {
                if (!string.IsNullOrEmpty(paginationSettings.Where))
                    command += $" WHERE {paginationSettings.Where}";
            }
            command += $" ORDER BY {paginationSettings.OrderColumn.FullName}";
            if (paginationSettings.OrderColumn.IsDesc)
                command += $" DESC";
            command += $" LIMIT {paginationSettings.Limit}";
            return command;
        }
    }
}
