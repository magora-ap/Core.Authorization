using System;
using Core.Authorization.Common.Models.Types;

namespace Core.Authorization.Common.Models.Repository
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? TimeZoneId { get; set; }
        public int? GenderId { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public DateTime? DeleteTimestamp { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public string Salt { get; set; }
        public bool IsConfirm { get; set; }
        public Guid? PhotoId { get; set; }

        public AuthJsonModel SocialData { get; set; }

        public GeographyPoint LocationFrom { get; set; }
        public MetaInfoModel MetaInfo { get; set; }
    }
}
