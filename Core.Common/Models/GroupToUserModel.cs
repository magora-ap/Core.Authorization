using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Authorization.Common.Models
{
    public class GroupToUserModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int GroupId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
