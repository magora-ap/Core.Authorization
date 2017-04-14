using Core.Authorization.Common.Concrete.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Core.Authorization.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Reference")]
    public class ReferenceController : Controller
    {
        public ReferenceController(IOptionsSnapshot<ConfigurationSettings> options)
        {
            var t = options.Value;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}