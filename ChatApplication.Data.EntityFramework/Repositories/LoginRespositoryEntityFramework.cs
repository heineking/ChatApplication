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
                .FirstOrDefault(l => l.Login == name && l.Password == password);
        }
    }
}
