using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Repositories;

namespace ChatApplication.Data.Contracts.Persistence
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected UnitOfWork(IUserRepository users, IRoomRepository rooms, IMessageRepository messages)
        {
            Users = users;
            Rooms = rooms;
            Messages = messages;
        }

        public abstract void Dispose();
        public abstract int SaveChanges();

        public IUserRepository Users { get; }
        public IRoomRepository Rooms { get; }
        public IMessageRepository Messages { get; }
    }
}
