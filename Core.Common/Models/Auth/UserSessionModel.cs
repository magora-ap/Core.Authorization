using System;
using System.Collections.Generic;
using Core.Authorization.Common.Concrete.Helpers;

namespace Core.Authorization.Common.Models.Auth
{
    /// <summary>
    /// Model for user session
    /// </summary>
    public class UserSessionModel
    {

        public bool IsExpirated { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public KeyValuePair<int, KeyValuePair<int, KeyValuePair<int, bool>[]>[]>[] SystemAccess { get; set; }

        public UserAuthModel UserModel { get; set; }
        public long ExpirationTime { get; set; }

    }
    
    [Serializable]
    public class UserAuthModel
    {
        public Guid UserId { get; set; }
        public IEnumerable<Enums.Group> Groups { get; set; }
        public TimeSpan? Offset { get; set; }
        public TimeSpan TimeOffset => Utils.ConvertOffset(Offset);
    }
}
