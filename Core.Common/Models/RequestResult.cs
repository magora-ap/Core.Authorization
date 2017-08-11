using Core.Authorization.Common.Models.Response.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Authorization.Common.Models
{
    public static class RequestResult
    {
        public static readonly ApiDescription RequestBodyNull = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.BadRequest,
                CodeString = "data_is_null"
            },
            Description = "Request body is null or empty"
        };

        public static readonly ApiDescription ErrorsNotExist = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.InternalServerError,
                CodeString = "errors_lost"
            },
            Description = "Request not valid but errors not exist"
        };

        public static readonly ApiDescription InvalidLoginOrPassword = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Unauthorized,
                CodeString = "sec.invalid_auth_data",
                GlobalCode = "security_error",
                Message = "User doesn't exist or password is wrong"
            },
            Description = "User doesn't exist or password is wrong"
        };

        public static readonly ApiDescription InvalidRefreshToken = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Unauthorized,
                CodeString = "sec.refresh_token_invalid",
                GlobalCode = "security_error",
                Message = "Refresh token isn't invalid"
            },
            Description = "Refresh token isn't invalid"
        };

        

        public static readonly ApiDescription IncorrectPassword = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "reg.password_incorrect",
                GlobalCode = "security_error",
                Message = "Incorrect password"
            },
            Description = "Incorrect password"
        };

        public static readonly ApiDescription IncorrectLogin = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "reg.login_incorrect",
                GlobalCode = "security_error",
                Message = "Incorrect login"
            },
            Description = "Incorrect login"
        };

        public static readonly ApiDescription Success = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.OK
            }
        };


    }
}
