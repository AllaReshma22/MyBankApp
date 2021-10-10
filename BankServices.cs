using System;
using System.Collections.Generic;
using System.Linq;
using BankApp;
using BankApp.Models;

namespace BankApp.Services
{
    public class BankServices
    {
        int acnumber = 1;
        public List<Account> accountslist = new List<Account>();
        public List<Transaction> transaclist = new List<Transaction>();
        public void createAccount(string acname, int Pin, int initialbal)
        {
            Account account = new Account
            {
                AccountHolderName = acname,
                pin = Pin,
                AccountNumber = acnumber,
                Balance = initialbal,
            };
            this.accountslist.Add(account);
            acnumber++;
        }
        public bool Deposit(int amount, int accountnumber, int pin)
        {
            var Accno = this.accountslist.Single(m => m.AccountNumber == accountnumber);
            if (Accno.pin == pin)
            {
                Accno.Balance += amount;
                AddTransaction(accountnumber, "deposit", DateTime.Now.ToString(), amount);
            }
            else
            {
                throw new Exception("Incorrect pin number");
            }
            return true;
        }
        public bool WithDraw(int amount, int accountnumber, int pin)
        {
            var Accno = this.accountslist.Single(m => m.AccountNumber == accountnumber);
            if (Accno.pin == pin)
            {
                Accno.Balance -= amount;
                AddTransaction(accountnumber, "withdraw", DateTime.Now.ToString(), amount);
            }
            else
            {
                throw new Exception("Incorrect pin number");
            }
            return true;

        }
      public bool transferAmount(int amount,int saccnumber,int spin,int raccnumber)
        {
            var Accno = this.accountslist.Single(m => m.AccountNumber == saccnumber);
            if (Accno.pin == spin)
            {
                if (Accno.Balance > amount)
                {
                    Accno.Balance -= amount;
                    var Accno1 = this.accountslist.Single(m => m.AccountNumber == raccnumber);
                    Accno1.Balance += amount;
                    AddTransaction(saccnumber, "withdraw", DateTime.Now.ToString(), amount);
                    AddTransaction(raccnumber, "deposit", DateTime.Now.ToString(), amount);


                }
                else
                    throw new Exception("Insufficient balance");
            }
            else
            {
                throw new Exception("Invalid pin number");
            }
            return true;
            
           
        }
        public int remBalance(int accountnumber, int pin)
        {

            var Accno = this.accountslist.Single(m => m.AccountNumber == accountnumber);
            if (Accno.pin == pin)
                return Accno.Balance;
            else
                throw new Exception("Invalid pin number");
        }
        public void AddTransaction(int toid,string type,string Datetime,int amount)
        {
            
            Transaction transaction = new(toid, type, amount, Datetime);
            transaclist.Add(transaction);
        }
        
        }
        
  }


