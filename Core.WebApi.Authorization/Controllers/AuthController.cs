using System;
using Core.Authorization.Bll.Abstract;
using Core.Authorization.Bll.Models.Helpers;
using Core.Authorization.Common;
using Core.Authorization.Common.Concrete.Extensions;
using Core.Authorization.Common.Models.Auth;
using Core.Authorization.Common.Models.Request.Auth;
using Core.Authorization.Common.Models.Response.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Authorization.WebApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthHelper _authHelper;

        public AuthController(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        [HttpPost]
        [Route("api/registration")]
        public ResultInfo Registration([FromBody] SignUpRequestModel model)
        {
            var data = _authHelper.RegistrationUser(new RegistrationRequestModel<SiteAuthModel>
            {
                Data = new SiteAuthModel
                {
                    Email = model.Login,
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
        [Route("api/auth/token")]
        public ResultInfo<AuthResponseModel> Authenticate([FromBody] LoginRequestModel model)
        {
            var data = _authHelper.Authenticate(new AuthenticateRequestModel<SiteAuthModel>
            {
                Data = new SiteAuthModel
                {
                    Password = model.Password,
                    Email = model.Login
                },
                Offset = model.Meta?.TimeOffset?.Offset.AsTimeSpan(),
            });
            return new ResultInfo<AuthResponseModel>
            {
                Code = ResponseResult.Success.Code.CodeString,
                Data = data
            };
        }

        [HttpPut]
        [Route("api/auth/token")]
        public ResultInfo<AuthResponseModel> RefreshToken([FromBody] RefreshTokenRequestModel model)
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