using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using Autofac;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Core.Authorization.WebApi.Filters
{
    public class AuthAttribute : AuthorizeAttribute
    {
        //private IEnumerable<Enums.Group> RolesEnum { get; set; }

        //protected override bool IsAuthorized(HttpActionContext actionContext)
        //{
        //    // ...
        //    return false;
        //}

        //protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        //{
        //    // ...
        //}

        //public AuthAttribute(Enums.Group[] roles)
        //{
        //    RolesEnum = roles;
        //}

        public AuthAttribute()
        {

        }
    }
}