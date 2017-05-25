using System;

namespace Core.Authorization.Common.Models
{
    public class BaseItemModel
    {
        public Guid Id { get; set; }
        public DateTime CreateTimestamp { get; set; }
    }
}
