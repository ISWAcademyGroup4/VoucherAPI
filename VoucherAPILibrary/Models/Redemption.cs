using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Models
{
    public class Redemption
    {
        public List<RedemptionUser> RedemptionUserList { get; set; }
        public int RedemptionCount { get; set; }
        public int RedeemedCount { get; set; }
        public int RedeemedAmount { get; set; }
    }
}
