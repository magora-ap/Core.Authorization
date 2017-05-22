using System;
using System.Collections.Generic;
using System.Text;
using Core.Authorization.Common.Models.Group;
using Core.Authorization.Dal.Models;

namespace Core.Authorization.Bll.Models.Helpers
{
    public class RegistrationResultModel
    {
        public UserModel User { get; set; }
        public IEnumerable<GroupModel> Groups { get; set; }
    }
}
