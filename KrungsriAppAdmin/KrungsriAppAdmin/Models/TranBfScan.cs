using System;
using System.Collections.Generic;
using System.Text;

namespace KrungsriAppAdmin.Models
{
    public class TranBfScan
    {
        public int AdminId { get; set; }
        public string MoneyAmount { get; set; }
        public string Ref { get; set; }
        public string ExpiryDate { get; set; }
    }
}
