using System;
using ChatApplication.Data.Contracts.Repositories;

namespace ChatApplication.Data.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        IUserRepository Users { get; }
        IRoomRepository Rooms { get; }
        IMessageRepository Messages { get; }
    }
}
