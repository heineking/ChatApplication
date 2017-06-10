using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Repositories;

namespace ChatApplication.Data.Contracts.Events
{
    public interface IDataEventWriterHandler<in TEntity> : IRepositoryWriter<TEntity> where TEntity : class
    {
        void Save();
    }
}
