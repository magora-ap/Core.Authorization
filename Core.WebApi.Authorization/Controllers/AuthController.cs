using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Authorization.Bll.Abstract;
using Core.Authorization.Bll.Models.Helpers;
using Core.Authorization.Common;
using Core.Authorization.Common.Concrete.Extensions;
using Core.Authorization.Common.Models.Auth;
using Core.Authorization.Common.Models.Request;
using Core.Authorization.Common.Models.Request.Auth;
using Core.Authorization.Common.Models.Response.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Authorization.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthHelper _authHelper;

        //public AuthController(IAuthHelper authHelper)
        //{
        //    _authHelper = authHelper;
        //}

        [HttpPost]
        [Route("api/registration")]
        //[Validator(ValidationKeys.AuthSiteRequest)]
        public ResultInfo Registration(SignUpRequestModel model)
        {
            var data = _authHelper.RegistrationUser(new RegistrationRequestModel<SiteAuthModel>
            {
                Data = new SiteAuthModel
                {
                    Email = model.Email,
                    Password = model.Password
                },
                Groups = model.Group == e_GroupAuthRequest.Admin ? new[] { Enums.Group.Administrator } : new[] { Enums.Group.User }
            });
            return new ResultInfo<Guid>
            {
                Code = ResponseResult.Success.Code.CodeString
            };
        }

        [HttpPost]
        [Route("api/authorization")]
        public ResultInfo<AuthResponseModel> Authenticate(LoginRequestModel model)
        {
            var data = _authHelper.Authenticate(new AuthenticateRequestModel<SiteAuthModel>
            {
                Data = new SiteAuthModel
                {
                    Password = model.Password,
                    Email = model.Email
                },
                Offset = model.TimeOffset?.Offset.AsTimeSpan(),
            });
            return new ResultInfo<AuthResponseModel>
            {
                Code = ResponseResult.Success.Code.CodeString,
                Data = data
            };
        }

        [HttpPost]
        [Route("api/token/refresh")]
        public ResultInfo<AuthResponseModel> RefreshToken(RefreshTokenRequestModel model)
        {
            var data = _authHelper.RefreshToken(model);
            return new ResultInfo<AuthResponseModel>
            {
                Code = ResponseResult.Success.Code.CodeString,
                Data = data
            };
        }

    }
}