using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Authorization.Common.Models
{
    public class BaseItemModel
    {
        public Guid Id { get; set; }
        public DateTime CreateTimestamp { get; set; }
    }
}
