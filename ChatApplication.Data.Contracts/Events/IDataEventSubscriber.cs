using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Events
{
    public interface IDataEventSubscriber<TEntity> where TEntity : class
    {
        void Created(DataEvent<TEntity> dataEvent);

        void Deleted(DataEvent<TEntity> dataEvent);

        void Read(DataEvent<TEntity> dataEvent);

        void Updated(DataEvent<TEntity> dataEvent);

        void Execute();
    }
}
