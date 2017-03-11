using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.Dapper.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Data.EntityFramework.Persistence;
using ChatApplication.Data.EntityFramework.Repositories;

namespace ChatApplication.Data.DapperEF.Persistence
{
    public static class EntityFrameworkDapperUnitOfWork
    {
        public static IUnitOfWork CreateEntityFrameworkDapperUnitOfWork()
        {
            
        }
    }
}
