using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class MessageRepository : Repository<MessageRecord>, IMessageRepository
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }
        public ChatContext ChatContext => Context as ChatContext;
    }
}
