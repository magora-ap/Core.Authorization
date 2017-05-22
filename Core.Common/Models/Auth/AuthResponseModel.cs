namespace Core.Authorization.Common.Models.Auth
{
    public class AuthResponseModel
    {
        public UserInfoModel UserInfo { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long AccessTokenExpire { get; set; }
    }
}
