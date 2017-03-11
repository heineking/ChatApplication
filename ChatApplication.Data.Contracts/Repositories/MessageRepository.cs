using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.Contracts.Repositories
{
    public class MessageRepository : Repository<MessageRecord>, IMessageRepository
    {
        public MessageRepository(IRepositoryReader<MessageRecord> reader, IRepositoryWriter<MessageRecord> writer) : base(reader, writer) { }
    }
}
