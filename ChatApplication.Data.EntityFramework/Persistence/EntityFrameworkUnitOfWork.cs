using System.Data.Entity;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;

namespace ChatApplication.Data.EntityFramework.Persistence
{
    public class EntityFrameworkUnitOfWork : UnitOfWork
    {
        private readonly ChatContext _context;

        public EntityFrameworkUnitOfWork(DbContext context, IUserRepository users, IRoomRepository rooms, IMessageRepository messages)
            : base(users, rooms, messages)
        {
            _context = (ChatContext)context;
        }

        public override void Dispose()
        {
            _context.Dispose();
        }

        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
