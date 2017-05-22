namespace Core.Authorization.Common.Models.Response.Http
{
    public class ResultInfo
    {
        public ResultInfo(string code = "200")
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}