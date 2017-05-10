using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.Infrastructure.Contracts;

namespace ChatApplication.API.Modules.Room
{
    public class LoginRequest
    {
        [JsonLogging]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}