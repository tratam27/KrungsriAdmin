using System;
using System.Collections.Generic;
using System.Text;

namespace KrungsriAppAdmin.Models
{
    public class MonthlyTrans
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public decimal MoneyAmount { get; set; }
        public string Ref { get; set; }
        public string UpdateDateTime { get; set; }
    }
}
