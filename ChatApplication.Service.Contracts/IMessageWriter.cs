using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Service.Contracts
{
    public interface IMessageWriter
    {
        void Save(Message message);
    }
}
