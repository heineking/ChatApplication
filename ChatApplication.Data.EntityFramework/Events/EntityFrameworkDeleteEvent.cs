using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Events;

namespace ChatApplication.Data.EntityFramework.Events
{
    public class EntityFrameworkDeleteEvent<TEntity> : IEvent where TEntity : class
    {
        public TEntity Entity { get; set; }
        public string Name { get; private set; }

        public EntityFrameworkDeleteEvent(TEntity entity, string eventName)
        {
            Entity = entity;
            Name = eventName;
        }
    }
}
