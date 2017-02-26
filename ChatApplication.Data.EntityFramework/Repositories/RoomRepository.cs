using System.Data.Entity;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class RoomRepository : Repository<RoomRecord>, IRoomRepository
    {
        public RoomRepository(DbContext context) : base(context)
        {
        }
        public ChatContext ChatContext => Context as ChatContext;
    }
}
