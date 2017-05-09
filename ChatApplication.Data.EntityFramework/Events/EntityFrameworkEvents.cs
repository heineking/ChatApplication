using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.EntityFramework.Events
{
    public static class EntityFrameworkEvents
    {
        public static string Delete(Type entityType) => $"entityFramework/DELETE_{entityType.Name}";
        public static string Add(Type entityType) => $"entityFramework/ADD_{entityType.Name}";
    }
}
