using Core.Authorization.Common.Models.Request.Timezone;

namespace Core.Authorization.Common.Models.Request.Auth
{
    public class RefreshTokenRequestModel
    {
        public string RefreshToken { get; set; }

        public TimeOffsetRequestModel TimeOffset { get; set; }
    }
}
