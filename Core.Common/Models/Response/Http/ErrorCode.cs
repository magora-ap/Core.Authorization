using System.Net;

namespace Core.Authorization.Common.Models.Response.Http
{
    public class ApiDescription
    {
        public ApiCode Code { get; set; }
        public string Description { get; set; }
        public string Field { get; set; }
    }

    public class ApiCode
    {
        public string CodeString { get; set; }
        public HttpStatusCode HttpCode { get; set; }
    }
}
