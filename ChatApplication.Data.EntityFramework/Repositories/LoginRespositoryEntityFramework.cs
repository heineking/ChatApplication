using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class LoginRespositoryEntityFramework : RepositoryEF<LoginRecord>, ILoginReader
    {
        private ChatContext ChatContext => (ChatContext)Context;

        public LoginRespositoryEntityFramework(DbContext context) : base(context)
        {
        }

        public LoginRecord LoginByNameAndPasswordOrDefault(string name, string password)
        {
            return ChatContext
                .Logins
                .Include(l => l.User)
                .FirstOrDefault(l => l.Username == name && l.Password == password);
        }

        public LoginRecord LoginByNameOrDefault(string name)
        {
            return ChatContext
                .Logins
                .Include(l => l.User)
                .Include(l => l.User.UserClaims)
                .Include(l => l.User.UserClaims.Select(u => u.Claim))
                .FirstOrDefault(l => l.Username == name);
        }
    }
}
