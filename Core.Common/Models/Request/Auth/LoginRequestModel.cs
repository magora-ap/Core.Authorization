using Core.Authorization.Common.Models.Request.Timezone;

namespace Core.Authorization.Common.Models.Request.Auth
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public TimeOffsetRequestModel TimeOffset { get; set; }
    }
}
