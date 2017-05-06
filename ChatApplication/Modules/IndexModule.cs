using Nancy;

namespace ChatApplication.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => View["index.html"];
            Get["/room/{roomId:long}"] = _ => View["index.html"];
        }
    }
}