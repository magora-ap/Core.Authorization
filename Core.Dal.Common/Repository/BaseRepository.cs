using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Core.Authorization.Common.Concrete.Helpers;
using Core.Dal.Common.Extension;
using Core.Dal.Common.Models;
using Npgsql;

namespace Core.Dal.Common.Repository
{
    /// <summary>
    ///     Help for work PostgreSql database
    /// </summary>
    /// <typeparam name="TModel">Type out from database</typeparam>
    /// <typeparam name="TResult">Primary key field type</typeparam>
    /// Initial author Sergey Sushenko
    public abstract class BaseRepository<TModel, TResult> : BaseSmallRepository
    {
        protected abstract string TableName { get; }
        protected virtual string Namespace => "public";
        protected string FullTableName => $"\"{Namespace}\".{TableName}";

        protected BaseRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected virtual string GetSelectAllCommand()
        {
            var command = $"{GetSelectAllWithoutFromCommand()} FROM {FullTableName}";
            return command;
        }

        protected virtual string GetSelectAllWithoutFromCommand()
        {
            var columns = GetColumns();
            columns = columns.Select(t => new ColumnItem($"\"{TableName}\".{t.Name}", t.IsPrimary));
            return $"SELECT {string.Join(",", columns.ToStringColumns())}";
        }

        protected virtual string GetCountAllCommand()
        {
            var command =
                $"SELECT COUNT (1) FROM {FullTableName}";
            return command;
        }

        protected virtual string GetDeleteAllCommand()
        {
            return $"DELETE FROM {FullTableName}";
        }

        protected abstract IEnumerable<ColumnItem> GetColumns();

        protected abstract IEnumerable<ValueItem> GetValues(TModel model);

        protected abstract TResult MapResult(Dictionary<string, object> row);

        protected abstract TModel MapToModel(Dictionary<string, object> row);


        protected TModel SelectOne(string field, string value)
        {
            var columns = GetColumns();
            var command =
                $"SELECT {string.Join(",", columns.ToStringColumns())} FROM {FullTableName} where {field}='{Utils.NormalizeString(value)}' LIMIT 1";
            return MapToModel(RunCommand(command).FirstOrDefault());
        }

        protected TModel SelectOneByPrimaryKey(string value)
        {
            var key = GetColumns().GetPrimaryColumn();
            return SelectOne(key.Name, value);
        }

        protected TResult UpdateItem(TModel model)
        {
            if (model == null) return default(TResult);
            var columns = GetColumns().ToArray();
            var colItems = columns.ToStringColumns().ToArray();
            var values = GetValues(model).ToArray();
            var valItems = values.ToStringValues().ToArray();
            var strings = new List<string>();
            for (var i = 0; i < columns.Count(); i++)
            {
                if (columns[i].NotUpdate) continue;
                strings.Add($"{colItems[i]} = {valItems[i]}");
            }

            var command =
                $"UPDATE {FullTableName} SET {string.Join(", ", strings)} WHERE {columns.GetPrimaryColumn().Name} = {values.GetPrimaryValue().Value} RETURNING {columns.GetPrimaryColumn().Name}";
            return MapResult(RunCommand(command).FirstOrDefault());
        }

        protected TResult Insert(TModel model)
        {
            return InsertMany(new[] { model }).FirstOrDefault();
        }

        protected TResult Upsert(TModel model, string[] conflictColumns)
        {
            return UpsertMany(new[] { model }, conflictColumns).FirstOrDefault();
        }

        protected IEnumerable<TResult> UpsertMany(IEnumerable<TModel> models, IEnumerable<string> conflictColumns)
        {
            if (!models.Any()) return new List<TResult>();
            var str = new List<string>();
            foreach (var model in models)
                str.Add($"({string.Join(", ", GetValues(model).Select(t => !t.IsAutoInc ? t.Value : "DEFAULT"))})");
            var command =
                $"INSERT INTO {FullTableName} ({string.Join(",", GetColumns().ToStringColumns())}) VALUES {string.Join(", ", str)} ON CONFLICT ({string.Join(", ", conflictColumns)}) DO NOTHING RETURNING {GetColumns().GetPrimaryColumn().Name}";
            return RunCommand(command).Select(MapResult);
        }

        protected IEnumerable<TResult> InsertMany(IEnumerable<TModel> models)
        {
            if (!models.Any()) return new List<TResult>();
            var str = new List<string>();
            foreach (var model in models)
                str.Add($"({string.Join(", ", GetValues(model).Select(t => !t.IsAutoInc ? t.Value : "DEFAULT"))})");
            var command =
                $"INSERT INTO {FullTableName} ({string.Join(",", GetColumns().ToStringColumns())}) VALUES {string.Join(", ", str)} RETURNING {GetColumns().GetPrimaryColumn().Name}";
            return RunCommand(command).Select(MapResult);
        }

        protected void DeleteMany(IEnumerable<string> ids)
        {
            if (!ids.Any()) return;
            var str = new List<string>();
            var primaryName = GetColumns().GetPrimaryColumn().Name;
            foreach (var id in ids)
                str.Add($"{primaryName}= '{id}'");
            var command = $"DELETE FROM {FullTableName} WHERE {string.Join(" OR ", str)}";
            RunCommand(command);
        }

        protected void Delete(string id)
        {
            var command = $"DELETE FROM {FullTableName} WHERE {GetColumns().GetPrimaryColumn().Name}='{id}'";
            RunCommand(command);
        }

        protected IEnumerable<TModel> GetPagination(string command, PaginationSettings paginationSettings)
        {
            return RunCommand(GetPaginationCommand(command, paginationSettings)).Select(MapToModel);
        }

        protected IEnumerable<TModel> GetPaginationDefaultCommand(PaginationSettings paginationSettings)
        {
            return GetPagination(GetSelectAllCommand(), paginationSettings);
        }
    }
}
