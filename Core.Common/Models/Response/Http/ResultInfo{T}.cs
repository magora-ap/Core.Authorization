using System;

namespace Core.Authorization.Common.Models.Response.Http
{
    public sealed class ResultInfo<T> : ResultInfo
    {
        public T Data { get; set; }
    }


    public sealed class ErrorResultInfo : ResultInfo
    {
        public ErrorInfo[] Errors { get; set; }    
    }

    public class ErrorInfo
    {
        public string Code { get; set; }

        public string Field { get; set; }

        public string Message { get; set; }
    }
}