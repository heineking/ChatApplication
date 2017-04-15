using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Models
{
    public class ClaimRecord
    {
        public long ClaimId { get; set; }
        public string ClaimName { get; set; }
        public List<UserClaimsRecord> UserClaims { get; set; }
    }
}
