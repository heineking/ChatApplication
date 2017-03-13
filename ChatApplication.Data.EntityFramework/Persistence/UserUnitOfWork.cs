using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;

namespace ChatApplication.Data.EntityFramework.Persistence
{
    public class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly ChatContext _context;

        public UserUnitOfWork(DbContext context, IUserRepository userRepository, ILoginRepository loginRepository)
        {
            _context = (ChatContext)context;
            Users = userRepository;
            Logins = loginRepository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IUserRepository Users { get; set; }
        public ILoginRepository Logins { get; set; }
    }
}
