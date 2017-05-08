using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using ChatApplication.Data.EntityFramework.Logging;

namespace ChatApplication.Data.EntityFramework.ContextEF
{
    public class ChatDbConfiguration : DbConfiguration
    {
        public ChatDbConfiguration()
        {
            DbInterception.Add(new LoggingInterceptor());
        }
    }
}
