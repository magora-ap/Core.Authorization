using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Authorization.Common.Concrete.Helpers;
using Newtonsoft.Json;

namespace Core.Dal.Common.Models
{
    /// <summary>
    /// Class for add values in database
    /// </summary>
    /// Initial author Sergey Sushenko
    public class ValueItem
    {
        public string Value { get; private set; }
        public bool IsPrimary { get; set; }
        public bool IsAutoInc { get; set; }

        public ValueItem(string value)
        {
            Value = string.IsNullOrEmpty(value) ? "NULL" : $"'{Utils.NormalizeString(value)}'";
        }

        public ValueItem(DateTime? value)
        {
            Value = !value.HasValue ? "NULL" : $"'{value.Value:yyyy-MM-dd HH:mm:ss}'";
        }

        public ValueItem(Guid value)
        {
            Value = $"'{value}'";
        }
        public ValueItem(Guid? value)
        {
            Value = !value.HasValue ? "NULL" : $"'{value}'";
        }
        public ValueItem(int value)
        {
            Value = $"{value}";
        }
        public ValueItem(int? value)
        {
            Value = !value.HasValue ? "NULL" : $"{value}";
        }
        public ValueItem(long value)
        {
            Value = $"{value}";
        }
        public ValueItem(long? value)
        {
            Value = !value.HasValue ? "NULL" : $"{value}";
        }

        public ValueItem(decimal value)
        {
            Value = $"{value.ToString(CultureInfo.InvariantCulture)}";
        }

        public ValueItem(decimal? value)
        {
            Value = !value.HasValue ? "NULL" : $"{value.Value.ToString(CultureInfo.InvariantCulture)}";
        }

        public ValueItem(bool? value)
        {
            Value = !value.HasValue ? "NULL" : $"'{value}'";
        }

        public ValueItem(TimeSpan value)
        {
            Value = $"'{value.TotalSeconds + " seconds"}'";
        }

        public ValueItem(TimeSpan? value)
        {
            Value = !value.HasValue ? "NULL" : $"'{value.Value.TotalSeconds + " seconds"}'";
        }
        public ValueItem(object value)
        {
            Value = value == null ? "NULL" : $"'{JsonConvert.SerializeObject(value)}'";
        }
    }
}
