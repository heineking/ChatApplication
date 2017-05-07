using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Reflection;
using ChatApplication.Logging;
using log4net;

namespace ChatApplication.Data.EntityFramework.Logging
{
    public class LoggingInterceptor : IDbCommandInterceptor
    {
        private static readonly ILog Log4Net = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            // Log4Net.Sql($"");
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            throw new NotImplementedException();
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            throw new NotImplementedException();
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            throw new NotImplementedException();
        }
    }
}
