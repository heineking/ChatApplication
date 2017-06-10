using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Syncronization.Contracts.Commands;

namespace ChatApplication.Syncronization.Out.Offsite.Comands
{

    public class Delete<TEntity> : ICommand where TEntity : class
    {
        protected readonly TEntity Entity;

        public Delete(TEntity entity)
        {
            Entity = entity;
        }

        public void Execute()
        {  
        }

        public void Retry()
        {

        }

        public void Undo()
        {

        }
    }
}
