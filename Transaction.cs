using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Transaction
    {
        public Transaction(int TargetAccount, string Type, double Amount, string Datetime)
        {
            this.TargetAccount = TargetAccount;
          this.Type = Type;
            this. Amount = Amount;
            this.Datetime = Datetime;
        }

        public int TargetAccount { get; set; }
           
      public string Type { get; set; }
        public double Amount { get; set; }
        public string Datetime { get; set; }
      public enum type
        {
            Deposit=1,
            Withdraw,
        }   
    }
}
