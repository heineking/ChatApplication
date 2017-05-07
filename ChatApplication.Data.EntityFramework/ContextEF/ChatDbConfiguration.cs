﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using ChatApplication.Data.EntityFramework.Logging;

namespace ChatApplication.Data.EntityFramework.ContextEF
{
    public class ChatDbConfiguration : DbConfiguration
    {
        public ChatDbConfiguration()
        {
            SetDatabaseLogFormatter((ctx, writer) => new LogFormatter(ctx, writer));
            // DbInterception.Add(new LoggingInterceptor());
        }
    }
}
