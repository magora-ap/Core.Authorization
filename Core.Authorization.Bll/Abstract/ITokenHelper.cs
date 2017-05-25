namespace Core.Authorization.Bll.Abstract
{
    public interface ITokenHelper
    {
        bool CheckAccessToken(string accessToken);
    }
}
