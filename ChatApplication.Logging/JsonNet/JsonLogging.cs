using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatApplication.Logging.JsonNet
{
    public static class JsonLogging
    {
        public static string Serialize<TEntity>(TEntity entity)
        {
            return JsonConvert.SerializeObject(entity, new JsonSerializerSettings
            {
                ContractResolver = LoggingContractResolver<TEntity>.Instance()
            });
        }
    }
}
