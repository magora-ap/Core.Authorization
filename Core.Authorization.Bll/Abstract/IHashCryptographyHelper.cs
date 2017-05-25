namespace Core.Authorization.Bll.Abstract
{
    public interface IHashCryptographyHelper
    {
        string GetMd5Hash(string input);

        string GetSha256Hash(string input);

        string GetSha512Hash(string input);

        string GetSalt();

        string GetSaltPassword(string password, string salt);

        string GetPassword(int length = 4);
    }
}
