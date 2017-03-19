using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.API.Extensions
{
    public static class NancyExtensions
    {
        public static void EnableCORS(this Nancy.Bootstrapper.IPipelines pipelines)
        {
            // source: http://stackoverflow.com/questions/15658627/is-it-possible-to-enable-cors-using-nancyfx/29322285#29322285
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                if (ctx.Request.Headers.Keys.Contains("Origin"))
                {
                    var origins = "" + string.Join(" ", ctx.Request.Headers["Origin"]);
                    ctx.Response.Headers["Access-Control-Allow-Origin"] = origins;

                    if (ctx.Request.Method == "OPTIONS")
                    {
                        // handle CORS preflight request

                        ctx.Response.Headers["Access-Control-Allow-Methods"] =
                            "GET, POST, PUT, DELETE, OPTIONS";

                        if (ctx.Request.Headers.Keys.Contains("Access-Control-Request-Headers"))
                        {
                            var allowedHeaders = "" + string.Join(
                                ", ", ctx.Request.Headers["Access-Control-Request-Headers"]);
                            ctx.Response.Headers["Access-Control-Allow-Headers"] = allowedHeaders;
                        }
                    }
                }
            });
        }
    }
}