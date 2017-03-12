using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.API.Security;
using JWT;
using Nancy;

namespace ChatApplication.API.Modules
{
    public class AuthModule : NancyModule
    {
        public AuthModule(IJwtEncoder encoder, UserAuth userAuth) : base("api/v1/auth")
        {
            Get["/"] = _ => encoder.Encode(ApiSecurity.Create(), userAuth.Key);
            Get["/plain"] = _ => ApiSecurity.Create();
        }
    }
}