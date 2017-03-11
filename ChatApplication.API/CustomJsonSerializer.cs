using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ChatApplication.API
{
    public sealed class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
       }
    }
}