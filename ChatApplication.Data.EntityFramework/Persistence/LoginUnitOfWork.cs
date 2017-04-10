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
    public class LoginUnitOfWork : ILoginUnitOfWork
    {
        private readonly ChatContext _context;

        public LoginUnitOfWork(DbContext context, ILoginRepository loginRepository)
        {
            _context = (ChatContext)context;
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
        
        public ILoginRepository Logins { get; set; }
    }
}
