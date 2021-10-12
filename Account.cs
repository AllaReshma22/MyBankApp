using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Account
    {
        public Account(int accountNumber, string accountHolderName, int password, double balance)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            Password = password;
            Balance = balance;
            Transactions = new List<Transaction>();
        }

        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }

        public int Password { get; set; }
        public double Balance { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}

