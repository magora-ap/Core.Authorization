namespace Core.Dal.Common.Models
{
    public class ColumnItem
    {
        public ColumnItem(string name, bool isPrimary = false)
        {
            Name = name;
            IsPrimary = isPrimary;
        }

        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool NotUpdate { get; set; }
    }
}
