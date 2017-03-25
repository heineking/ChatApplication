using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.API.Modules.Room
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}