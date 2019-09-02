using System;
using System.Collections.Generic;
using System.Text;

namespace KrungsriAppAdmin.Models
{
    public class LoginModel
    {
        public string token { get; set; }
        public string BookBank { get; set; }
        public string RefreshToken { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
    }
}
