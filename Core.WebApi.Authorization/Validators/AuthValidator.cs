using Core.Authorization.Common.Models;
using Core.Authorization.Common.Models.Request.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Authorization.WebApi.Validators
{
    public class AuthValidator : AbstractValidator<LoginRequestModel>
    {
        public AuthValidator()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Matches("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}")
                .WithMessage(RequestResult.InvalidLoginOrPassword.Description)
                .WithState(x => RequestResult.InvalidLoginOrPassword.Code);

            RuleFor(x => x.Login)
                .NotNull()
                .NotEmpty()
                .WithMessage(RequestResult.InvalidLoginOrPassword.Description)
                .WithState(x => RequestResult.InvalidLoginOrPassword.Code);


        }
    }
}
