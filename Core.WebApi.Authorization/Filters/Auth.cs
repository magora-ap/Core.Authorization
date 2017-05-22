using Core.Authorization.Common.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Authorization.WebApi.Filters
{
    //public class Auth : AuthorizationHandler<Auth>, IAuthorizationRequirement
    //{

        
    //    public override Task HandleAsync(AuthorizationHandlerContext context)
    //    {

    //        return base.HandleAsync(context);
    //    }
    //    public override void Handle(AuthResponseModel context, Auth requirement)
    //    {

    //        return;
    //    }
    //}
}


//public class Over18Requirement : AuthorizationHandler<Over18Requirement>, IAuthorizationRequirement
//{
//    public override void Handle(AuthorizationContext context, Over18Requirement requirement)
//    {
//        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
//        {
//            context.Fail();
//            return;
//        }

//        var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
//        int age = DateTime.Today.Year - dateOfBirth.Year;
//        if (dateOfBirth > DateTime.Today.AddYears(-age))
//        {
//            age--;
//        }

//        if (age >= 18)
//        {
//            context.Succeed(requirement);
//        }
//        else
//        {
//            context.Fail();
//        }
//    }
//}
//}