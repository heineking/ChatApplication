using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Logging;
using log4net;

namespace ChatApplication.Data.EntityFramework.Logging
{
    public class EntityFrameworkLogger
    {
        private static readonly ILog Log4Net = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void Log(string message) => Log4Net.Sql(message);
    }

    public class LogFormatter : DatabaseLogFormatter
    {
        public LogFormatter(Action<string> writeAction) : base(writeAction)
        {
        }
        public LogFormatter(DbContext context, Action<string> writeAction) : base(context, writeAction)
        {
        }
        public override void LogCommand<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            Write($"Context '{Context.GetType().Name}' is executing command '{Environment.NewLine}{command.CommandText}{Environment.NewLine}");
        }
    }
}
