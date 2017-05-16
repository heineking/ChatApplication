using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApplication.Syncronization.Mock
{
    public class MockSync
    {
        public static void Sync()
        {
            var rand = new Random();
            var wait = rand.Next(0, 5);
            var exception = rand.Next(1, 10) == 10;
            Thread.Sleep(wait);
            if (wait > 3)
            {
                throw new TimeoutException("syncing timeout");
            }
            if (exception)
            {
                throw new Exception("sync failed");
            }
        }
    }
}
