using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Transaction
    {
        public Transaction(int TargetAccount, double Amount,TransactionType.transactionType type, DateTime datetime)
        {
            this.TargetAccount = TargetAccount;
          
            this. Amount = Amount;
            Type=type;
            dateTime = datetime;
        }
      
        public int TargetAccount { get; set; }
        public double Amount { get; set; }
        public  DateTime dateTime { get; set; }
        public TransactionType.transactionType Type { get; set; }
     
    }
}
