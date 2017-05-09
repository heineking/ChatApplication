using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Logging.JsonNet;
using ChatApplication.Logging.Profiling;
using log4net;
using Newtonsoft.Json;

namespace ChatApplication.Logging
{
    public class Profiler : IProfiler
    {
        private readonly IStopwatch _stopwatch;
        public string Type { protected get; set; }
        public ILog Log { set; protected get; }

        public Profiler(IStopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }
        public TResult Profile<TResult>(Func<TResult> profiledFunction, string prependLog = null)
        {
            _stopwatch.Start();
            var result = profiledFunction();
            var ms = _stopwatch.Stop();
            Log.Repo($"{prependLog} type=[{Type}]; ms=[{ms}];");
            return result;
        }

        public void Profile(Action profiledFunction, string prependLog = null)
        {
            Profile(
                () =>
                {
                    profiledFunction();
                    return 0;
                },
                prependLog
            );
        }

        public string Serialize<TEntity>(TEntity entity)
        {
            return JsonConvert.SerializeObject(entity, new JsonSerializerSettings
            {
                ContractResolver = LoggingContractResolver<TEntity>.Instance(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
        }
    }
}
