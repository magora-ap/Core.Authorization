namespace Core.Authorization.Common.Models.Auth.Token
{
    public class RefreshTokenModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public long ExpirationTime { get; set; }
    }
}
