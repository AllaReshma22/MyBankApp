using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class Bank
    {
        public List<Account> AccountsList { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
     public Bank()
        {
            AccountsList = new List<Account>();
        }
    }
}

