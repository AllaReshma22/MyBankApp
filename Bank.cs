using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    
    public class Account
    {
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
       
        public int pin { get; set; }
        public int Balance { get; set; }

        }
    public class Transaction
    {
        public Transaction(int toid, string type, int amount, string datetime)
        {
            this.toid = toid;
            Type = type;
            Amount = amount;
            Datetime = datetime;
        }
        public int toid { get; set; }
        public int fromid { get; set; }
        public string Type { get; set; }   
        public int Amount { get; set; }
        public string Datetime { get; set; }
       
    }
    }

