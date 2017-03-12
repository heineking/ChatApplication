using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ChatApplication.API.Security
{
    public static class ApiSecurity
    {
        public static string CreateKey()
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);
            return key;
        }

        public static Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                { "claim1", 0 },
                { "claim2", "claim2-value" }
            };
        }
    }
}