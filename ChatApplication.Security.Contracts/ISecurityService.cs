using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Security.Contracts
{
    public interface ISecurityService
    {
        LoginToken LoginTokenOrDefault(string name, string password);
        string EncodeToken(LoginToken token);
        LoginToken DecodeToken(string jwt);
    }
}
