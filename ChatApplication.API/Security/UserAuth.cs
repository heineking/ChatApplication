using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JWT;
using JWT.Algorithms;

namespace ChatApplication.API.Security
{
    public class UserAuth
    {
        public string Key { get; protected set; }
        public IJwtAlgorithm Algorithm { get; protected set; }

        public UserAuth()
        {
            Key = ApiSecurity.CreateKey();
            Algorithm = new HMACSHA256Algorithm();
        }
    }
}