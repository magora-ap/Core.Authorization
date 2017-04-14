﻿namespace Core.Authorization.Common.Models.Helpers
{
    public class GoogleUserModel
    {

        public string id { get; set; }
        public string email { get; set; }
        public bool verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string locale { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string hd { get; set; }
    }
}
