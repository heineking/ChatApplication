using System;

namespace ChatApplication.Data.Contracts.Events
{
    public static class EventName
    {
        public static string Delete(Type entityType) => $"data/DELETE_{entityType.Name}";
        public static string Add(Type entityType) => $"data/ADD_{entityType.Name}";
    }
}
