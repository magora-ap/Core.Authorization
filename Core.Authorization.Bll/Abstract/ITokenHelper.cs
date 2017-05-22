using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Authorization.Bll.Abstract
{
    public interface ITokenHelper
    {
        bool CheckAccessToken(string accessToken);
    }
}
