using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;

namespace ChatApplication.Data.DapperEF.Persistence
{
    public class UnitOfWorkDecorated : IUnitOfWork
    {
        private readonly IUnitOfWork _uow;

        public UnitOfWorkDecorated(IUnitOfWork uow, IRoomRepository roomRepository)
        {
            _uow = uow;
            Users = _uow.Users;
            Messages = _uow.Messages;
            Rooms = roomRepository;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public int SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public IUserRepository Users { get; }
        public IRoomRepository Rooms { get; }
        public IMessageRepository Messages { get; }
    }
}
