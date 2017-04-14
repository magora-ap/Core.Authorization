namespace Core.Authorization.Common.Models.Auth
{
    /// <summary>
    /// Model for authorization user
    /// </summary>
    /// Initial author Sergey Sushenko
    public class AuthResponseModel
    {
        public UserInfoModel UserInfo { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long AccessTokenExpire { get; set; }
    }
}
