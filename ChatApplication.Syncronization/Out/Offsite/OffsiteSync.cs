using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Syncronization.Out.Offsite.Comands;

namespace ChatApplication.Syncronization.Out.Offsite
{
    public class OffsiteSync<TEntity> : IDataEventWriterHandler<TEntity> where TEntity : class
    {
        private readonly List<TEntity> _created;
        private readonly List<TEntity> _updated;
        private readonly List<TEntity> _deleted;

        public OffsiteSync()
        {
            _created = new List<TEntity>();
            _updated = new List<TEntity>();
            _deleted = new List<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _created.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _created.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            if (_created.Remove(entity)) return;
            _deleted.Add(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_created.Remove(entity)) continue;
                _deleted.Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            _updated.Add(entity);
        }

        public void Save()
        {
            var commands = new List<Action>();
            var createCommands = _created.Select(entity => (Action) new Create<TEntity>(entity).Execute);
            var deleteCommands = _deleted.Select(entity => (Action) new Delete<TEntity>(entity).Execute);

            commands.AddRange(createCommands);
            commands.AddRange(deleteCommands);

            Parallel.ForEach(commands, command => command());
        }
    }
}
