using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChatApplication.Infrastructure.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ChatApplication.Logging.JsonNet
{
    public class LoggingContractResolver<TEntity> : DefaultContractResolver
    {
        private readonly IList<string> _propertyNames;

        public LoggingContractResolver()
        {
            _propertyNames = typeof(TEntity)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(JsonLoggingAttribute)))
                .Select(p => p.Name)
                .ToList();
        }
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var jsonProperties = base.CreateProperties(type, memberSerialization).ToList();
                
            return jsonProperties
                .Where(jp => _propertyNames.Contains(jp.PropertyName))
                .ToList();
        }

        // cache the contract resolver for performance reasons
        private static LoggingContractResolver<TEntity> _instance;

        public static LoggingContractResolver<TEntity> Instance()
        {
            return _instance ?? (_instance = new LoggingContractResolver<TEntity>());
        }
    }
}