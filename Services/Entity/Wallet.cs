using Services.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entity
{
    public class Wallet
    {
        public string Id { get; set; }
        public string Name { get; set;}
        public int Balance { get; set;}
        public int BalanceHistory { get; set;}
        public Status Status { get; set;}
        public User User { get; set; }
    }
}
