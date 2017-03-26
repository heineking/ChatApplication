using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using Dapper;

namespace ChatApplication.Data.Dapper.Repositories
{
    public class RoomRepositoryReader : IRepositoryReader<RoomRecord>
    {
        private readonly string _connectionString;

        public RoomRepositoryReader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RoomRecord Get(long id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var room = db.Query<RoomRecord>("SELECT * from RoomRecords where RoomId = @id", new {id});
                db.Close();
                return room.FirstOrDefault();
            }
        }

        public IEnumerable<RoomRecord> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var rooms = db.Query<RoomRecord>("SELECT * from RoomRecords");
                db.Close();
                return rooms;
            }
        }

        public IEnumerable<RoomRecord> Find(Expression<Func<RoomRecord, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
