using Core.Authorization.Bll.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Core.Authorization.Bll.Abstract;
using Core.Authorization.Bll.Models.Helpers;
using Core.Authorization.Common;
using Core.Authorization.Common.Abstract;
using Core.Authorization.Common.Models.Auth;
using Core.Authorization.Common.Models.Auth.Token;
using Microsoft.AspNetCore.Http;

namespace Core.Authorization.WebApi.Filters
{
    public class AuthHandler : AuthorizationHandler<AuthRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpContext Context => _contextAccessor.HttpContext;

        private ITokenHelper TokenHelper { get; }
        private ICacheStoreHelper<UserAuthModel> CacheStoreHelper { get; }

        private IEnumerable<Enums.Group> RolesEnum { get; set; }

        public AuthHandler(IHttpContextAccessor contextAccessor,
            ITokenHelper tokenHelper,
            ICacheStoreHelper<UserAuthModel> cacheStoreHelper)
        {
            _contextAccessor = contextAccessor;
            TokenHelper = tokenHelper;
            CacheStoreHelper = cacheStoreHelper;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthRequirement requirement)
        {
            var resources = context.Resource;
            if (resources != null)
            {
                StringValues tokens;
                var filterContext = resources as FilterContext;
                if (filterContext != null && !filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out tokens))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                var token = tokens.First();
                int index = token.IndexOf("Bearer ", StringComparison.Ordinal);
                string cleanToken = (index < 0)
                    ? token
                    : token.Remove(index, "Bearer ".Length);
                var res = TokenHelper.CheckAccessToken(cleanToken);
                if (!res)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                var tokenPayload = Bll.Helpers.TokenHelper.GetPayloadByJwtToken<AccessTokenModel>(cleanToken).model;
                if (tokenPayload != null)
                {
                    var model = CacheStoreHelper[CommonConstants.AccessTokenPrefix + cleanToken];
                    if (model == null)
                    {
                        context.Fail();
                        return Task.CompletedTask;
                    }
                    if (RolesEnum != null && RolesEnum.Any())
                    {
                        if (!model.Groups.Any(t => RolesEnum.Any(f => f == t)))
                        {
                            context.Fail();
                            return Task.CompletedTask;
                        }
                    }

                    var principal = new UserPrincipal(new GenericIdentity(tokenPayload.UserId.ToString()), new string[0])
                    {
                        UserModel = model
                    };

                    Context.User = principal;

                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
