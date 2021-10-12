using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class Bank
    {
        public List<Account> AccountsList { get; set; }
     public Bank()
        {
            AccountsList = new List<Account>();
        }
    }
}

