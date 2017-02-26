using Nancy;

namespace ChatApplication.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => View["index.cshtml"];
        }
    }
}