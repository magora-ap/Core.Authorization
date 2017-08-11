using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Core.Authorization.Common.Api;
using Core.Authorization.Common.Models.Response.Http;
using FluentValidation.Results;
using Core.Authorization.Common.Models;
using Core.Authorization.Bll.Helpers;
using FluentValidation;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Core.Authorization.WebApi.Filters
{
    public enum ValidationKeys
    {
        AuthRequest,
        RefreshToken,
        Registration
    }
    public class ValidatorAttribute : ActionFilterAttribute
    {
        public ValidationKeys Key;

        private Func<ValidationKeys, IValidator> _validatorFactory;

        

        public ValidatorAttribute(Func<ValidationKeys, IValidator> validatorFactory, ValidationKeys key)
        {
            _validatorFactory = validatorFactory;
            Key = key;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var argumentValue = actionContext.ActionArguments.First().Value;
            if (argumentValue == null)
            {
                actionContext.Result = GetRequestBodyNullResponse(actionContext);
                return;
            }

            var validator = _validatorFactory(Key);
            var validationResult = validator.Validate(argumentValue);
            if (validationResult.IsValid)
            {
                return;
            }

            actionContext.Result = GetErrorResponse(actionContext, validationResult);
        }

        protected static IActionResult GetErrorResponse(ActionExecutingContext actionContext, ValidationResult validationResult)
        {
            var firstError = validationResult.Errors.First().CustomState as ApiCode;
            if (firstError == null)
            {
                return GetErrorsNotExistResponse(actionContext);
            }

            return new JsonResult(new ResultErrorInfo
            {
                Code = firstError.GlobalCode,
                Errors = validationResult
                    .Errors
                    .Select(x => new ErrorModel
                    {
                        Code = (x.CustomState as ApiCode).CodeString,
                        Message = x.ErrorMessage,
                        Field = x.PropertyName
                    })
                    .ToArray(),
                Message = firstError.Message
            });
        }

        protected static IActionResult GetRequestBodyNullResponse(ActionExecutingContext actionContext)
        {
            return new JsonResult(new ResultErrorInfo
            {
                Code = RequestResult.RequestBodyNull.Code.CodeString,
                Errors = new[]
                {
                    new ErrorModel
                    {
                        Code = RequestResult.RequestBodyNull.Code.CodeString,
                        Message = RequestResult.RequestBodyNull.Description,
                        Field = RequestResult.RequestBodyNull.Field
                    }
                }
            });
        }

        protected static IActionResult GetErrorsNotExistResponse(ActionExecutingContext actionContext)
        {
            return new JsonResult(new ResultErrorInfo
            {
                Code = RequestResult.ErrorsNotExist.Code.CodeString,
                Errors = new[]
                {
                    new ErrorModel
                    {
                        Code = RequestResult.ErrorsNotExist.Code.CodeString,
                        Message = RequestResult.ErrorsNotExist.Description,
                        Field = RequestResult.ErrorsNotExist.Field
                    }
                }
            });
        }
    }
}
