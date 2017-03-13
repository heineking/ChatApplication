using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Security.Contracts
{
    public class Login
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
    }
}
