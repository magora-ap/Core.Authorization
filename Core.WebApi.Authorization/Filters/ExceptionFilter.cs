using System;
using System.Linq;
using Core.Authorization.Common.CustomException;
using Core.Authorization.Common.Models.Response.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace Core.Authorization.WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext context)
        {
            //logger.Error(actionExecutedContext.Exception);

            if (context.Exception is CredentialWrong)
            {
                var response = new ErrorResultInfo
                {
                    Code = ResponseResult.BadParameters.Code.CodeString,
                    Errors = new[]
                    {
                        new ErrorInfo
                        {
                            Code = ResponseResult.BadParameters.Code.CodeString,
                            Message = "Wrong login or password"
                        }
                    }
                };
                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = (int)ResponseResult.BadParameters.Code.HttpCode;
                return;
            }
            //if (actionExecutedContext.Exception is UserNotExist)
            //{
            //    var response = actionExecutedContext.Request.CreateResponse(ResponseResult.NotFound.Code.HttpCode,
            //        new ErrorResultInfo
            //        {
            //            Code = ResponseResult.NotFound.Code.CodeString,
            //            Errors = new[]
            //            {
            //                new ErrorInfo
            //                {
            //                    Code = ResponseResult.NotFound.Code.CodeString,
            //                    Message = "User not exist"
            //                }
            //            }
            //        });
            //    actionExecutedContext.Response = response;
            //    return;
            //}
            if (context.Exception is UserAlreadyExist)
            {
                var response = new ErrorResultInfo
                        {
                            Code = ResponseResult.UserAlreadyExist.Code.CodeString,
                            Errors = new[]
                            {
                                new ErrorInfo
                                {
                                    Code = ResponseResult.UserAlreadyExist.Code.CodeString,
                                    Message = ResponseResult.UserAlreadyExist.Description
                                }
                            }
                        };
                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = (int) ResponseResult.UserAlreadyExist.Code.HttpCode;
                return;
            }

            if (context.Exception is PasswordNotSecurity)
            {
                var response = new ErrorResultInfo
                        {
                            Code = ResponseResult.PasswordNotSecurity.Code.CodeString,
                            Errors = new[]
                            {
                                new ErrorInfo
                                {
                                    Code = ResponseResult.PasswordNotSecurity.Code.CodeString,
                                    Message = ResponseResult.PasswordNotSecurity.Description
                                }
                            }
                        };
                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = (int) ResponseResult.PasswordNotSecurity.Code.HttpCode;
                return;
            }
            if (context.Exception is AcccountDeactivatedException)
            {
                var response = new ErrorResultInfo
                        {
                            Code = ResponseResult.AccountDeactivated.Code.CodeString,
                            Errors = new[]
                            {
                                new ErrorInfo
                                {
                                    Code = ResponseResult.AccountDeactivated.Code.CodeString,
                                    Message = ResponseResult.AccountDeactivated.Description
                                }
                            }
                        };
                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = (int) ResponseResult.AccountDeactivated.Code.HttpCode;
                return;
            }

            var res = new ErrorResultInfo
                    {
                        Code = ResponseResult.InternalError.Code.CodeString,
                        Errors = new[]
                        {
                            new ErrorInfo
                            {
                                Code = ResponseResult.InternalError.Code.CodeString,
                                Message = context.Exception.Message
                            }
                        }
                    };
            context.Result = new JsonResult(res);
            context.HttpContext.Response.StatusCode = (int) ResponseResult.InternalError.Code.HttpCode;
        }
    }
}