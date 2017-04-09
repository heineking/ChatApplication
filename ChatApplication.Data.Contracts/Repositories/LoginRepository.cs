using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.Contracts.Repositories
{
    public class LoginRepository : Repository<LoginRecord>, ILoginRepository
    {
        public LoginRepository(ILoginReader reader, IRepositoryWriter<LoginRecord> writer) : base(reader, writer)
        {
        }
    }
}
