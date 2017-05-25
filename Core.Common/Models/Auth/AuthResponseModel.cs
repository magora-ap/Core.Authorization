namespace Core.Authorization.Common.Models.Auth
{
    public class AuthResponseModel
    {
        public string AccessToken { get; set; }
        public long AccessTokenExpire { get; set; }
        public string RefreshToken { get; set; }

        public UserInfoModel AuthInfo { get; set; }
    }
}

