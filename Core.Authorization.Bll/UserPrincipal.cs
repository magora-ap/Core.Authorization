using System.Security.Principal;
using Core.Authorization.Common.Models.Auth;

namespace Core.Authorization.Bll.Models.Helpers
{
    public class UserPrincipal : GenericPrincipal
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Security.Principal.GenericPrincipal" /> class from a user
        ///     identity and an array of role names to which the user represented by that identity belongs.
        /// </summary>
        /// <param name="identity">
        ///     A basic implementation of <see cref="T:System.Security.Principal.IIdentity" /> that represents
        ///     any user.
        /// </param>
        /// <param name="roles">
        ///     An array of role names to which the user represented by the <paramref name="identity" /> parameter
        ///     belongs.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null. </exception>
        public UserPrincipal(IIdentity identity,
            string[] roles) : base(identity, roles)
        {
        }

        public UserAuthModel UserModel { get; set; }
    }
}
