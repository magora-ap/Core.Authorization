using System;

namespace Core.Authorization.Common.Models.Auth.Token
{
    public class AccessTokenModel
    {
        public string AccessToken { get; set; }

        public long ExpirationTime { get; set; }

        public Guid UserId { get; set; }

    }
}
