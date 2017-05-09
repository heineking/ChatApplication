using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Syncronization.Contracts.Commands
{
    public interface IReversibleCommand : ICommand
    {
        void Undo();
    }
}
