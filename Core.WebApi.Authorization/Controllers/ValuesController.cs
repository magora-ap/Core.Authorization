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
        //private IAuthorizationService _authorizationService;
        //public ValuesController(IUserRepository repository, IAuthorizationService authorizationService)
        //{
        //    //var t = repository;
        //    _authorizationService = authorizationService;
        //}

        // GET api/values
        [HttpGet]
        [Authorize(Policy = "Auth")]
        public IEnumerable<string> Get()
        {
            var userId = (HttpContext.User as UserPrincipal).UserModel.UserId;

            return new string[] { userId.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
