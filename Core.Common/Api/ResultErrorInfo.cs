using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Authorization.Common.Api
{
    public class ResultErrorInfo : ResultInfo
    {
        public ErrorModel[] Errors { get; set; }
        public string Message { get; set; }
    }

    public class ErrorModel
    {
        public string Code { get; set; }
        
        public string Message { get; set; }
        
        public string Field { get; set; }
    }

    public class BusinessException : Exception
    {
        public ResultErrorInfo ErrorInfo { get; set; }
    }

    public class ResultInfo
    {
        #region Properties


        public ResultInfo()
        {

        }

        public string Code { get; set; } = "200";

        #endregion
    }
}
