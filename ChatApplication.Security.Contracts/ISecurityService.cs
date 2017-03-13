using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Security.Contracts
{
    public interface ISecurityService
    {
        LoginToken ValidateLogin(string name, string password);
        string EncodeToken(LoginToken token);
        LoginToken DecodeToken(string jwt);
    }
}
