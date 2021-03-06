﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Security.Contracts
{
    public class LoginToken
    {
        // JWT reservered claims
        public string Iss { get; set; } // issuer
        public DateTime Exp { get; set; } // expiration

        // ChatApplication information
        public string LoginName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Claims { get; set; }
    }
}
