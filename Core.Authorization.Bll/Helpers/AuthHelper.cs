using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Autofac;
using Core.Authorization.Bll.Abstract;
using Core.Authorization.Bll.Models.Helpers;
using Core.Authorization.Common;
using Core.Authorization.Common.Abstract;
using Core.Authorization.Common.Concrete.Extensions;
using Core.Authorization.Common.CustomException;
using Core.Authorization.Common.Models.Auth;
using Core.Authorization.Common.Models.Auth.Token;
using Core.Authorization.Common.Models.Group;
using Core.Authorization.Common.Models.Helpers;
using Core.Authorization.Common.Models.Request.Auth;
using Core.Authorization.Dal.Abstract;
using Core.Authorization.Dal.Models;

namespace Core.Authorization.Bll.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        public AuthHelper(ILifetimeScope scope,
            IUserRepository userRepository,
            IHashCryptographyHelper hashCryptographyHelper,
            ICacheStoreHelper<UserAuthModel> cacheStoreHelper)
            //IBaseItemRepository baseItemRepository,
            //IGroupToUserRepository groupToUserRepository,
            //IGroupRepository groupRepository)
        {
            Scope = scope;
            UserRepository = userRepository;
            HashCryptographyHelper = hashCryptographyHelper;
            CacheStoreHelper = cacheStoreHelper;
            //TokenHelper = tokenHelper;
            //BaseItemRepository = baseItemRepository;
            //GroupToUserRepository = groupToUserRepository;
            //GroupRepository = groupRepository;         
        }

        private ILifetimeScope Scope { get; }

        private IUserRepository UserRepository { get; }
        private IHashCryptographyHelper HashCryptographyHelper { get; }
        private ICacheStoreHelper<UserAuthModel> CacheStoreHelper { get; }
        //private IBaseItemRepository BaseItemRepository { get; }
        //private IGroupToUserRepository GroupToUserRepository { get; }
        //private IGroupRepository GroupRepository { get; }
        //private ITokenHelper TokenHelper { get; }

        RegistrationResultModel IAuthHelper.RegistrationUser(RegistrationRequestModel<SiteAuthModel> model)
        {
            if (!CheckSecurityPassword(model?.Data?.Password)) throw new PasswordNotSecurity();
            var salt = HashCryptographyHelper.GetSalt();
            var userModel = new UserModel
            {
                Email = model.Data.Email,
                PasswordHash = !string.IsNullOrEmpty(model.Data.Password)
                    ? HashCryptographyHelper.GetSaltPassword(
                        HashCryptographyHelper.GetSha512Hash(model.Data.Password), salt
                    )
                    : null,
                Salt = salt,
                IsConfirm = false
            };
            return Registration(userModel, true, model.Groups);
        }

        public AuthResponseModel Authenticate(AuthenticateRequestModel<SiteAuthModel> auth)
        {
            var user = UserRepository.GetByEmail(auth.Data.Email.ToLowerInvariant());
            if (user == null)
                throw new CredentialWrong();
            if (!ValidateCredentials(auth))
                throw new CredentialWrong();
            if (!user.IsActive) throw new AcccountDeactivatedException();
            return Authenticate(user, auth.Offset);
        }

        public AuthResponseModel RefreshToken(RefreshTokenRequestModel model)
        {
            var token = TokenHelper.GetPayloadByJwtToken<RefreshTokenModel>(model.RefreshToken);
            var user = CacheStoreHelper[CommonConstants.RefreshTokenPrefix + model.RefreshToken];
            if (user == null) throw new CredentialWrong();
            CacheStoreHelper.Remove(token.model.RefreshToken);
            CacheStoreHelper.Remove(token.model.AccessToken);
            var session = CreateSession(user.UserId, user.Groups, model.TimeOffset?.Offset.AsTimeSpan());
            var userModel = UserRepository.GetById(user.UserId);
            if (userModel == null) throw new CredentialWrong();
            if (!userModel.IsActive) throw new AcccountDeactivatedException();
            //var groups = GroupRepository.GetGroupsByUserId(userModel.Id).ToArray();
            return new AuthResponseModel
            {
                AccessToken = session.AccessToken,
                AccessTokenExpire = session.ExpirationTime,
                RefreshToken = session.RefreshToken,
                AuthInfo = new UserInfoModel
                {
                    DisplayName = $"{userModel.LastName} {userModel.FirstName}",
                    UserId = userModel.Id,
                    Permissions = new List<string>(), // ToDo change
                    Email = userModel.Email,
                    AvatarId = userModel.PhotoId,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    //TimestampOfBirth = userModel.BirthDate.UnixDateTime(),
                    //Groups = groups.Select(t => new GroupInfoModel
                    //{
                    //    Id = t.Id,
                    //    Name = t.Name
                    //})
                }
            };
        }

        private AuthResponseModel Authenticate(UserModel user, TimeSpan? offset)
        {
            if (user == null)
                throw new CredentialWrong();
            if (user?.IsActive == false) throw new AcccountDeactivatedException();
            var groups = new List<GroupModel>(); // GroupRepository.GetGroupsByUserId(user.Id).ToArray();
            var session = CreateSession(user.Id, groups.Select(t => (Enums.Group) t.Id), offset);
            return new AuthResponseModel
            {
                AccessToken = session.AccessToken,
                AccessTokenExpire = session.ExpirationTime,
                RefreshToken = session.RefreshToken,
                AuthInfo = new UserInfoModel
                {
                    DisplayName = $"{user.FirstName} {user.LastName}",
                    UserId = user.Id,
                    Permissions = new List<string>(), // ToDo change

                    Email = user.Email,
                    AvatarId = user.PhotoId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    //TimestampOfBirth = user.BirthDate.UnixDateTime(),
                    Groups = groups.Select(t => new GroupInfoModel
                    {
                        Id = t.Id,
                        Name = t.Name
                    })
                }
            };
        }

        private RegistrationResultModel Registration(UserModel model, bool isConfirmEmail,
            IEnumerable<Enums.Group> groups)
        {
            using (var transaction = UserRepository.BeginTransaction())
            {
                var userExist = UserRepository.GetByEmail(model.Email.ToLowerInvariant());
                if (userExist != null) throw new UserAlreadyExist();
                //var baseItemId = BaseItemRepository.Create(new BaseItemModel
                //{
                //    CreateTimestamp = DateTime.UtcNow,
                //    Id = Guid.NewGuid()
                //});
                if (isConfirmEmail == false)
                    model.IsConfirm = true;
                model.Id = Guid.NewGuid();
                model.CreateTimestamp = DateTime.UtcNow;
                model.IsActive = true;

                //model.BaseId = baseItemId;
                var userId = UserRepository.Create(model);
                model.Id = userId;
                //GroupToUserRepository.CreateMany(groups.Select(t => new GroupToUserModel
                //{
                //    CreateDate = DateTime.UtcNow,
                //    UserId = userId,
                //    GroupId = (int) t
                //}));
                //Guid hash = Guid.Empty;
                //if (isConfirmEmail)
                //{
                //    hash = HashRepository.Create(new HashModel
                //    {
                //        Id = Guid.NewGuid(),
                //        CreateTimestamp = DateTime.UtcNow,
                //        IsActive = true,
                //        CreateUserId = userId,
                //        TypeId = (int)Enums.HashType.ConfirmEmail
                //    });
                //SendLetterConfirmation(model, hash);
                transaction.Commit();
            }
            
            return new RegistrationResultModel
            {
                User = model
            };
        }


        private bool CheckSecurityPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            var pattern = "(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}";
            return Regex.IsMatch(password, pattern);
        }

        private UserSessionModel CreateSession(Guid userId, IEnumerable<Enums.Group> groups, TimeSpan? offset)
        {
            var expirationPeriod = ConfigurationHelper.AccessTokenExpiratedPeriod;
            var expirationRefreshPeriod = ConfigurationHelper.RefreshTokenExpiratedPeriod;
            var userCacheModel = new UserAuthModel
            {
                UserId = userId,
                Groups = groups.ToArray(),
                Offset = offset
            };
            var accessToken = new AccessTokenModel
            {
                AccessToken = HashCryptographyHelper.GetSha512Hash(HashCryptographyHelper.GetPassword(16)),
                ExpirationTime = DateTime.Now.Add(expirationPeriod).UnixDateTime(),
                UserId = userId
            };

            var refreshToken = new RefreshTokenModel
            {
                RefreshToken = HashCryptographyHelper.GetSha512Hash(HashCryptographyHelper.GetPassword(16)),
                AccessToken = TokenHelper.CreateJwtToken(accessToken, ConfigurationHelper.JwtPublicKey),
                ExpirationTime = DateTime.Now.Add(expirationRefreshPeriod).UnixDateTime()
            };
            var model = new UserSessionModel
            {
                UserModel = userCacheModel,
                AccessToken = TokenHelper.CreateJwtToken(accessToken, ConfigurationHelper.JwtPublicKey),
                RefreshToken = TokenHelper.CreateJwtToken(refreshToken, ConfigurationHelper.JwtPublicKey),
                ExpirationTime = accessToken.ExpirationTime
            };
            CacheStoreHelper.Add(CommonConstants.AccessTokenPrefix + model.AccessToken, userCacheModel,
                expirationPeriod);
            CacheStoreHelper.Add(CommonConstants.RefreshTokenPrefix + model.RefreshToken, userCacheModel,
                expirationRefreshPeriod);
            return model;
        }

        public bool ValidateCredentials(AuthenticateRequestModel<SiteAuthModel> auth)
        {
            var user = UserRepository.GetByEmail(auth.Data.Email);
            return ValidateCredentials(auth.Data.Password, user.Id);
        }

        private bool ValidateCredentials(string password, Guid userId)
        {
            var user = UserRepository.GetById(userId);
            if (string.IsNullOrEmpty(user?.PasswordHash)) return false;
            var hashPassword =
                HashCryptographyHelper.GetSaltPassword(HashCryptographyHelper.GetSha512Hash(password),
                    user.Salt);
            return hashPassword.Equals(user.PasswordHash) && user.IsConfirm;
        }

    }

}

