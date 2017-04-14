using System;
using Core.Authorization.Common.Models.Repository;
using Core.Dal.Common.Abstract;

namespace Core.Authorization.Dal.Abstract
{
    public interface IUserRepository : ITransactionRepository
    {
        Guid Update(UserModel model);
        UserModel GetById(Guid id);
        UserModel GetByEmail(string email);
        UserModel GetByGoogleId(string id);
        UserModel GetByFacebookId(string id);
        Guid Create(UserModel model);
    }
}
