using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Security.Contracts
{
    public interface IPasswordService
    {
        string GeneratePasswordHash(string password);
        bool ValidatePassword(string hash, string password);
    }
}
