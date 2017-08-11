using Core.Authorization.Bll;
using Core.Authorization.Bll.Helpers;
using Core.Authorization.Common.Concrete.Helpers;
using Core.Authorization.Dal;
using Autofac;
using FluentValidation;
using Core.Authorization.WebApi.Filters;
using Core.Authorization.WebApi.Validators;
using System;

namespace Core.Authorization.WebApi.Run
{
    public sealed class ValidatorIoCModule : Module
    {
        #region Overridden Methods
        private ConfigurationSettings Settings { get; set; }



        public ValidatorIoCModule(ConfigurationSettings options)
        {
            Settings = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            /*builder.RegisterType(typeof(AuthValidator)).Keyed<IValidator>(ValidationKeys.AuthRequest).As<IValidator>();
            builder.RegisterType(typeof(RegistrationValidator)).Keyed<IValidator>(ValidationKeys.Registration).As<IValidator>();*/
            builder.Register<IValidator>((c, p) =>
            {
                var type = p.TypedAs<ValidationKeys>();
                switch (type)
                {
                    case ValidationKeys.Registration:
                        return new RegistrationValidator();
                    case ValidationKeys.AuthRequest:
                        return new AuthValidator();
                    default:
                        throw new ArgumentException("Invalid validator");
                }
            }).As<IValidator>();
        }

        #endregion
    }
}
