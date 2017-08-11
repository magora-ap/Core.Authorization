using System;
using System.Collections.Generic;

namespace Core.Authorization.Common.Models.Auth
{
    public class UserInfoModel
    {
        public string DisplayName { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        /*
        public string Email { get; set; }
        public Guid? AvatarId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<GroupInfoModel> Groups { get; set; }
        public long? TimestampOfBirth { get; set; }*/
    }

    public class GroupInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
