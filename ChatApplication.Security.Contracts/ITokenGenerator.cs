using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Security.Contracts
{
    public interface ITokenGenerator
    {
        LoginToken CreateLoginToken(LoginRecord loginRecord);
    }
}
