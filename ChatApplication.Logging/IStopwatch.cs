using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ChatApplication.Logging
{
    public interface IStopwatch
    {
        void Start();
        long Stop();
    }

    public interface IProfiler
    {
        string Type { set; }
        ILog Log { set; }

        TResult Profile<TResult>(Func<TResult> profiledFuntion, string prependLog = null);
        void Profile(Action profiledFunction, string prependLog = null);
        string Serialize<TEntity>(TEntity entity);
    }
}
