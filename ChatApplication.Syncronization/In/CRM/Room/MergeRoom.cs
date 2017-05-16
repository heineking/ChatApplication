using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Syncronization.Contracts.Commands;

namespace ChatApplication.Syncronization.In.CRM.Room
{
    public class MergeRoom : ICommand
    {
        private readonly RoomRecord _room;

        public MergeRoom(RoomRecord room)
        {
            _room = room;
        }
        public void Execute()
        {
            if (_room == null) return;
            _room.Name = _room.Name.ToUpper();
        }

        public void Retry()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
