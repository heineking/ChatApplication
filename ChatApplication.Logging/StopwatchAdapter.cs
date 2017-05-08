using System;
using System.Diagnostics;
using log4net;

namespace ChatApplication.Logging
{
    public class StopwatchAdapter : IStopwatch
    {
        private readonly Stopwatch _stopwatch;

        public StopwatchAdapter(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }

        public void Start()
        {
           _stopwatch.Start();
        }

        public long Stop()
        {
            _stopwatch.Stop();
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return elapsedMilliseconds;
        }
    }
}
