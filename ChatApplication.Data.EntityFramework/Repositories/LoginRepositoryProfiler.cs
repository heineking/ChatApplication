using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Logging.Profiling;
using log4net;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class LoginRepositoryProfiler : ILoginReader
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ILoginReader _readerDelegate;
        private readonly IProfiler _profiler;

        public LoginRepositoryProfiler(ILoginReader reader, IProfiler profiler)
        {
            _readerDelegate = reader;

            // set up the profiler
            _profiler = profiler;
            _profiler.Type = typeof(LoginRecord).Name;
            _profiler.Log = _log;
        }
        public LoginRecord Get(long id)
        {
            return _profiler.Profile(
                () => _readerDelegate.Get(id),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; id=[{id}]; "
             );
        }

        public IEnumerable<LoginRecord> GetAll()
        {
            return _profiler.Profile(
                () => _readerDelegate.GetAll(),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; "    
            );
        }

        public IEnumerable<LoginRecord> Find(Expression<Func<LoginRecord, bool>> predicate)
        {
            return _profiler.Profile(
                () => _readerDelegate.Find(predicate),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; predicate=[{predicate}]; "
            );
        }

        public LoginRecord LoginByNameAndPasswordOrDefault(string name, string password)
        {
            return _profiler.Profile(
                () => _readerDelegate.LoginByNameAndPasswordOrDefault(name, password),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; name=[{name}]; "
            );
        }

        public LoginRecord LoginByNameOrDefault(string name)
        {
            return _profiler.Profile(
                () => _readerDelegate.LoginByNameOrDefault(name),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; name=[{name}]; "
            );
        }
    }
}
