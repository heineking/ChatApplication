using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.Contracts.Repositories
{
    public class UserRepository : Repository<UserRecord>, IUserRepository
    {
        public UserRepository(IRepositoryReader<UserRecord> reader, IRepositoryWriter<UserRecord> writer) : base(reader, writer) { }
    }
}
