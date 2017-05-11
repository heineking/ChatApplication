namespace ChatApplication.Data.Contracts.Events
{
    public static class EventName<TEntity> where TEntity : class
    {
        public static string Delete => $"data/DELETE_{typeof(TEntity).Name}";
        public static string Created => $"data/CREATED_{typeof(TEntity).Name}";
        public static string Read => $"data/READ_{typeof(TEntity).Name}";
        public static string Updated => $"data/UPDATED_{typeof(TEntity).Name}";

        public static string Saved => "data/SAVED_CHANGES";
    }
}
