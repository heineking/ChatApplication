using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.API.Extensions
{
    public static class DateTimeJavascript
    {
        private static readonly long NanoUnit = 10000;

        private static readonly long UnixEpochTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        private static long MsToNano(this long timeInMs)
        {
            return timeInMs * NanoUnit;
        }

        public static DateTime ToCsharpDateTime(long jsTimeStamp)
        {
            var jsTimeStampTicks = jsTimeStamp.MsToNano();
            return new DateTime(jsTimeStampTicks + UnixEpochTicks);
        }
    }
}