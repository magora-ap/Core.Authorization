namespace Core.Dal.Common.Models
{
    /// <summary>
    /// Class for settings pagination
    /// </summary>
    /// Initial author Sergey Sushenko
    public class PaginationSettings
    {
        public int PageSize { get; set; }
        public int NumberPage { get; set; }
        public OrderItem OrderColumn { get; set; }
        public string Where { get; set; }
        public int Limit => PageSize;
        public int Offset => PageSize * NumberPage;
    }
}
