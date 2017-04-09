using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Repositories;

namespace ChatApplication.Data.Contracts.Persistence
{
    public interface ILoginUnitOfWork : IDisposable
    {
        int SaveChanges();
        IUserRepository Users { get; set; }
        ILoginRepository Logins { get; set; }
    }
}
