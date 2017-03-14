using System.Collections.Generic;
using System.Linq;
using Core.Dal.Common.Models;

namespace Core.Dal.Common.Extension
{
    /// <summary>
    /// Extension from custom repository types
    /// </summary>
    public static class ExtensionRepository
    {
        public static ColumnItem GetPrimaryColumn(this IEnumerable<ColumnItem> columns)
        {
            return columns.FirstOrDefault(t => t.IsPrimary);
        }

        public static IEnumerable<string> ToStringColumns(this IEnumerable<ColumnItem> columns)
        {
            return columns.Select(t => t.Name);
        }

        public static ValueItem GetPrimaryValue(this IEnumerable<ValueItem> values)
        {
            return values.FirstOrDefault(t => t.IsPrimary);
        }

        public static IEnumerable<string> ToStringValues(this IEnumerable<ValueItem> columns)
        {
            return columns.Select(t => t.Value);
        }

    }
}
