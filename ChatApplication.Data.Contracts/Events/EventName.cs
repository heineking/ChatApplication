namespace ChatApplication.Data.Contracts.Events
{
    public static class EventName<TEntity> where TEntity : class
    {
        private static string EntityName => typeof(TEntity).Name;

        public static string Deleted => $"data/DELETE_{EntityName}";
        public static string DeletedMany => $"data/DELETED_MANY_{EntityName}";

        public static string Updated => $"data/UPDATED_{EntityName}";

        public static string Created => $"data/CREATED_{EntityName}";
        public static string CreatedMany => $"data/CREATED_MANY_{EntityName}";

        public static string Read => $"data/READ_{EntityName}";
        public static string ReadAll => $"data/READ_ALL_{EntityName}";
        public static string Find => $"data/FIND_{EntityName}";

        public static string Saved => "data/SAVED_CHANGES";
    }
}
