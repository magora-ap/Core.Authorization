using System;

namespace Core.Authorization.Common.Models.Auth
{
    /// <summary>
    /// Model for store session
    /// </summary>
    public class SessionModel
    {
        public string AссessToken { get; set; }

        public string RefreshToken { get; set; }

        public string JwtToken { get; set; }

        public DateTime ExpirationTime { get; set; }

        public string DisplayName { get; set; }

        public Guid UserId { get; set; }
    }
}
