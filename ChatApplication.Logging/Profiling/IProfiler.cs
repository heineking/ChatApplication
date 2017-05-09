using System;
using log4net;

namespace ChatApplication.Logging.Profiling
{
    public interface IProfiler
    {
        string Type { set; }
        ILog Log { set; }

        TResult Profile<TResult>(Func<TResult> profiledFuntion, string prependLog = null);
        void Profile(Action profiledFunction, string prependLog = null);
        string Serialize<TEntity>(TEntity entity);
    }
}
