using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using Nancy.TinyIoc;

namespace ChatApplication.API.Configure
{
    public class RepoImplementations
    {
        public IRepositoryReader<RoomRecord> RoomReader { get; }
        public IRepositoryWriter<RoomRecord> RoomWriter { get; }
        public IRepositoryReader<MessageRecord> MessageReader { get; }
        public IRepositoryWriter<MessageRecord> MessageWriter { get; }
        public IRepositoryReader<UserRecord> UserReader { get; }
        public IRepositoryWriter<UserRecord> UserWriter { get; }
        public IRepositoryWriter<LoginRecord> LoginWriter { get; }
        public ILoginReader LoginReader { get; }

        public RepoImplementations(TinyIoCContainer container)
        {
            RoomReader = container.Resolve<IRepositoryReader<RoomRecord>>();
            RoomWriter = container.Resolve<IRepositoryWriter<RoomRecord>>();
            MessageReader = container.Resolve<IRepositoryReader<MessageRecord>>();
            MessageWriter = container.Resolve<IRepositoryWriter<MessageRecord>>();
            UserReader = container.Resolve<IRepositoryReader<UserRecord>>();
            UserWriter = container.Resolve<IRepositoryWriter<UserRecord>>();
            LoginReader = container.Resolve<ILoginReader>();
            LoginWriter = container.Resolve<IRepositoryWriter<LoginRecord>>();
        }
    }
}