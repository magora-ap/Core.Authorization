using Autofac;
using Core.Authorization.Common;
using Core.Authorization.Common.Abstract;
using Core.Authorization.Common.Concrete.Extensions;
using Core.Authorization.Common.Models.Auth;
using Core.Authorization.Common.Models.Auth.Token;
using Core.Authorization.Common.Models.Helpers;
using Jose;
using Newtonsoft.Json;
using System;
using Core.Authorization.Bll.Abstract;
using Core.Authorization.Dal.Abstract;

namespace Core.Authorization.Bll.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        public readonly ICacheStoreHelper<UserAuthModel> _cacheStoreHelper;

        public TokenHelper(ICacheStoreHelper<UserAuthModel> cacheStoreHelper)
        {
            _cacheStoreHelper = cacheStoreHelper;
        }

        public static string CreateGuidToken()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            return guid;
        }

        public static string CreateJwtToken<T>(T model, string key)
        {
            var userJson = JsonConvert.SerializeObject(model);
            string token = JWT.Encode(userJson, Convert.FromBase64String(key), JwsAlgorithm.HS256);
            return token;
        }

        public static PayloadModel<T> GetPayloadByJwtToken<T>(string token)
        {
            try
            {
                var key = Convert.FromBase64String(ConfigurationHelper.JwtPublicKey);
                var mod = JWT.Decode<T>(token, key);

                return new PayloadModel<T>
                {
                    model = mod
                };
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return default(PayloadModel<T>);
            }
        }

        public bool CheckAccessToken(string accessToken)
        {
            var payload = GetPayloadByJwtToken<AccessTokenModel>(accessToken);

            if (payload?.model?.ExpirationTime < DateTime.UtcNow.UnixDateTime())
            {
                return false;
            }

            //var cacheHelper = IoC.Instance.Resolve<ICacheStoreHelper<UserAuthModel>>();
            if (!_cacheStoreHelper.ContainsKey(CommonConstants.AccessTokenPrefix + accessToken) 
                || _cacheStoreHelper[CommonConstants.AccessTokenPrefix + accessToken].UserId != payload?.model?.UserId)
            {
                return false;
            }

            return true;
        }
    }
}
