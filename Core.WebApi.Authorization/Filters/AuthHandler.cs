using Core.Authorization.Bll.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
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
        private IHttpContextAccessor _contextAccessor;
        private HttpContext _context => _contextAccessor.HttpContext;
        private ITokenHelper _tokenHelper { get; }
        private IEnumerable<Enums.Group> RolesEnum { get; set; }
        public ICacheStoreHelper<UserAuthModel> _cacheStoreHelper;

        public AuthHandler(IHttpContextAccessor contextAccessor, ITokenHelper tokenHelper, ICacheStoreHelper<UserAuthModel> cacheStoreHelper)
        {
            _contextAccessor = contextAccessor;
            _tokenHelper = tokenHelper;
            _cacheStoreHelper = cacheStoreHelper;
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

                //var tokenHelper = new TokenHelper();
                var token = tokens.First();
                int index = token.IndexOf("Bearer ", StringComparison.Ordinal);
                string cleanToken = (index < 0)
                    ? token
                    : token.Remove(index, "Bearer ".Length);
                var res = _tokenHelper.CheckAccessToken(cleanToken);
                if (!res)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                var tokenPayload = TokenHelper.GetPayloadByJwtToken<AccessTokenModel>(cleanToken).model;
                if (tokenPayload != null)
                {
                    //using (var scope = IoC.BeginLifetimeScope())
                    //{
                    //var cacheStoreHelper = scope.Resolve<ICacheStoreHelper<UserAuthModel>>();
                    var model = _cacheStoreHelper[CommonConstants.AccessTokenPrefix + cleanToken];
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

                    _context.User = principal;

                    //var principal1 = new UserPrincipal(new GenericIdentity("asdbvbe22"), new string[0]) {
                    //    UserModel = new UserAuthModel() { UserId = Guid.NewGuid() }
                    //}; _context.User = principal1;

                    //Thread.CurrentPrincipal = principal;
                    //HttpContext.Current.User = principal;

                    context.Succeed(requirement);
                    return Task.CompletedTask;
                    //}
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
