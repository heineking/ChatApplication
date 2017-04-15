using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Models
{
    public class UserClaimsRecord
    {
        public long UserClaimsId { get; set; }
        public long UserId { get; set; }
        public long ClaimId { get; set; }
        public virtual UserRecord User { get; set;}
        public virtual ClaimRecord Claim { get; set; }
    }
}
