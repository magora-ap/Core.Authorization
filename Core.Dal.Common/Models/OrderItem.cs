namespace Core.Dal.Common.Models
{
    /// <summary>
    /// Class for setting order column
    /// </summary>
    /// Initial author Sergey Sushenko
    public class OrderItem
    {
        public string Name { get; set; }
        public bool IsDesc { get; set; }
        public string TableName { get; set; }
        public OrderItem(string name, string tableName, bool isDesc = false)
        {
            Name = name;
            IsDesc = isDesc;
            TableName = tableName;
        }

        public string FullName => $"\"{TableName}\".{Name}";

    }
}
