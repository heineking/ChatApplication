using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.Contracts.Repositories
{
    public class RoomRepository : Repository<RoomRecord>, IRoomRepository
    {
        public RoomRepository(IRepositoryReader<RoomRecord> reader, IRepositoryWriter<RoomRecord> writer) : base(reader, writer) { }
    }
}
