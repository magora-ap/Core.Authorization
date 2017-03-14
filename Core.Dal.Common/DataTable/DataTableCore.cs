using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace Core.Dal.Common.DataTable
{
    public class DataTableCore
    {
        public List<Dictionary<string, object>> Rows;

        public DataTableCore()
        {
            Rows=new List<Dictionary<string, object>>();
        }

        public void Load(NpgsqlDataReader reader)
        {
            while (reader.Read())
            {
                Rows.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
            }
        }
    }
}
