using System;
using System.Collections.Generic;
using System.Text;
using Core.Authorization.Common;

namespace Core.Authorization.Bll.Models.Helpers
{
    public class RegistrationRequestModel<T> : AuthenticateRequestModel<T>
    {
        public IEnumerable<Enums.Group> Groups { get; set; }
    }
}
