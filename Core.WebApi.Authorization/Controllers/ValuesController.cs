using System.Collections.Generic;
using Core.Authorization.Bll.Helpers;
using Core.Authorization.Bll.Models.Helpers;
using Core.Authorization.Dal.Abstract;
using Core.Authorization.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Core.Authorization.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ExceptionFilter]
    public class ValuesController : Controller
    {

        // GET api/values
        [HttpGet]
        [Authorize(Policy = "Auth")]
        public IEnumerable<string> Get()
        {
            var userId = (HttpContext.User as UserPrincipal).UserModel.UserId;

            return new string[] { userId.ToString() };
        }
    }
}
