using Core.Authorization.Common.Models.Auth;

namespace Core.Authorization.Common.Models.Request.Auth
{
    public class SignUpRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public e_GroupAuthRequest Group { get; set; }
    }
}
