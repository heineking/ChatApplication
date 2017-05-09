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
}
