using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace ChatApplication.API.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => View["index.cshtml"];
        }
    }
}