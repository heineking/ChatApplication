using System.Reflection;
using log4net;
using log4net.Core;

namespace ChatApplication.Logging
{
    public static class Log4NetExtensions
    {
        private static readonly Level SqlLevel = new Level(35000, "SQL");
        private static readonly Level SqlFatalLevel = new Level(35002, "SQL:Fatal");
        private static readonly Level TimingLevel = new Level(40000, "TIMING");
        private static readonly Level RepoLevel = new Level(40000, "REPO");

        public static void Sql(this ILog log, object message)
        {
            log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, SqlLevel, message, null);
        }

        public static void Timing(this ILog log, object message)
        {
            log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, TimingLevel, message, null);
        }

        public static void Repo(this ILog log, object message)
        {
            log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, RepoLevel, message, null);
        }

        public static void SqlFatal(this ILog log, object message)
        {
            log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, SqlFatalLevel, message, null);
        }
        public static void Configure()
        {
            // this probably isn't necessary...
            LogManager.GetRepository().LevelMap.Add(RepoLevel);
            LogManager.GetRepository().LevelMap.Add(SqlLevel);
            LogManager.GetRepository().LevelMap.Add(SqlFatalLevel);
            LogManager.GetRepository().LevelMap.Add(TimingLevel);
        }
    }
}
