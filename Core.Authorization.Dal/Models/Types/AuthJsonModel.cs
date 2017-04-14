using Core.Authorization.Common.Models.Helpers;

namespace Core.Authorization.Dal.Models.Types
{
    public class AuthJsonModel
    {
        public GoogleUserModel GoogleModel { get; set; }
        public FacebookUserModel FacebookModel { get; set; }
    }
}
