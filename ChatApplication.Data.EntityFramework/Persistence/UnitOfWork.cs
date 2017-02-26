using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Data.EntityFramework.Repositories;

namespace ChatApplication.Data.EntityFramework.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatContext _context;

        public UnitOfWork(ChatContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Rooms = new RoomRepository(context);
            Messages = new MessageRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IUserRepository Users { get; }
        public IRoomRepository Rooms { get; }
        public IMessageRepository Messages { get; }
    }
}
