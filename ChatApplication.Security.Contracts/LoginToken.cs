using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Security.Contracts
{
    public class LoginToken
    {
        // JWT reservered claims
        public string Iss { get; set; } // issuer
        public string Exp { get; set; } // expiration

        // ChatApplication information
        public string LoginName { get; set; }
        public Guid UserId { get; set; }
    }
}
