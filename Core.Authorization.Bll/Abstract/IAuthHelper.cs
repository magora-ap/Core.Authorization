using Core.Authorization.Bll.Models.Helpers;
using Core.Authorization.Common.Models.Auth;
using Core.Authorization.Common.Models.Request.Auth;

namespace Core.Authorization.Bll.Abstract
{
    public interface IAuthHelper
    {
        RegistrationResultModel RegistrationUser(RegistrationRequestModel<SiteAuthModel> model);
        AuthResponseModel Authenticate(AuthenticateRequestModel<SiteAuthModel> auth);
        AuthResponseModel RefreshToken(RefreshTokenRequestModel model);
    }
}
