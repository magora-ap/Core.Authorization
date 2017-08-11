using System;
using System.Linq;
using Core.Authorization.Common.CustomException;
using Core.Authorization.Common.Models.Response.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using Core.Authorization.Common.Models;
using Core.Authorization.Common.Api;

namespace Core.Authorization.WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is RefreshTokenWrong)
            {
                var response = new ErrorResultInfo
                {
                    Code = RequestResult.InvalidRefreshToken.Code.GlobalCode,
                    Errors = new[]
                    {
                        new ErrorInfo
                        {
                            Code = RequestResult.InvalidRefreshToken.Code.CodeString,
                            Message = RequestResult.InvalidRefreshToken.Code.Message,
                            Field = "refreshToken"
                        }
                    },
                    Message = RequestResult.InvalidRefreshToken.Code.Message
                };
                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = (int)ResponseResult.BadParameters.Code.HttpCode;
                return;
            }

            if (context.Exception is CredentialWrong)
            {
                var response = new ErrorResultInfo
                {
                    Code = RequestResult.InvalidLoginOrPassword.Code.GlobalCode,
                    Errors = new[]
                    {
                        new ErrorInfo
                        {
                            Code = RequestResult.InvalidLoginOrPassword.Code.CodeString,
                            Message = RequestResult.InvalidLoginOrPassword.Code.Message
                        }
                    },
                    Message = RequestResult.InvalidLoginOrPassword.Code.Message
                };
                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = (int)ResponseResult.BadParameters.Code.HttpCode;
                return;
            }

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