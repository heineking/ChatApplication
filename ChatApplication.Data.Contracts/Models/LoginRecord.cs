using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Models
{
    public class LoginRecord
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual UserRecord User { get; set; }
    }
}
