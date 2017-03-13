using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.Contracts.Repositories
{
    public interface ILoginReader : IRepositoryReader<LoginRecord>
    {
        LoginRecord ValidateLogin(string name, string password);
    }
}
