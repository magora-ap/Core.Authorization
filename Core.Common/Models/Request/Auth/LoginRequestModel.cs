using Core.Authorization.Common.Models.Request.Meta;

namespace Core.Authorization.Common.Models.Request.Auth
{
    public class LoginRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public MetaInfo Meta { get; set; }
    }
}
