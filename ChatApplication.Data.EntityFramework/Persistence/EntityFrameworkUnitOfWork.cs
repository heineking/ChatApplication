using System.Data.Entity;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Infrastructure.Contracts.Events;

namespace ChatApplication.Data.EntityFramework.Persistence
{
    public class EntityFrameworkUnitOfWork : UnitOfWork
    {
        private readonly ChatContext _context;
        private readonly IEventPublisher _publisher;
        public EntityFrameworkUnitOfWork(DbContext context, IEventPublisher publisher, IUserRepository users, IRoomRepository rooms, IMessageRepository messages)
            : base(users, rooms, messages)
        {
            _context = (ChatContext)context;
            _publisher = publisher;
        }

        public override void Dispose()
        {
            _context.Dispose();
        }

        public override int SaveChanges()
        {
            var result = _context.SaveChanges();
            _publisher.Publish(new SaveEvent());
            return result;
        }
    }
}
