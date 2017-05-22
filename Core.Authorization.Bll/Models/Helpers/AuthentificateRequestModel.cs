using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Authorization.Bll.Models.Helpers
{
    public class AuthenticateRequestModel<T>
    {
        public T Data { get; set; }
        public TimeSpan? Offset { get; set; }
    }
}
