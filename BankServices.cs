using System;
using System.Collections.Generic;
using System.Linq;
using BankApp;
using BankApp.Models;

namespace BankApp.Services
{
    public class BankServices
    {
        Bank bank = new Bank();
        public static int accountNumber = 1;
        public void createAccount( string AccountName, int password, double initialbal)
        {
            Account account = new Account(accountNumber, AccountName, password, initialbal);
            this.bank.AccountsList.Add(account);
            accountNumber++;
        }
        public bool Deposit(double amount, int accountnumber, int pin)
        {

            var account = this.bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);

            if (account is null)
            {
                throw new Exception("Either invalid");
            }
            else
            {
                if (account.Password == pin)
                {
                    account.Balance += amount;
                    AddTransaction(accountnumber, "deposit", DateTime.Now, amount);
                }
                else
                {
                    throw new Exception("Incorrect pin number");
                }
            }

            return true;
        }
        public bool WithDraw(double amount, int accountnumber, int pin)
        {
            var account = this.bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);

            if (account is null)
            {
                throw new Exception("Either invalid");
            }
            else
            {
                if (account.Password == pin)
                {

                    if (account.Balance < amount)
                    {
                        throw new Exception("Insufficient Balance");
                    }
                    else
                    {

                        account.Balance -= amount;
                        AddTransaction(accountnumber, "withdraw", DateTime.Now, amount);
                    }
                }


                else
                {
                    throw new Exception("Incorrect pin number");
                }
            }
            return true;

        }
        public bool transferAmount(double amount, int senderaccnumber, int pin, int receiveraccnumber)
        {
            var account = this.bank.AccountsList.SingleOrDefault(m => m.AccountNumber == senderaccnumber);

            if (account is null)
            {
                throw new Exception("Either invalid");
            }
            else
            {
                if (account.Password == pin)
                {

                    if (account.Balance > amount)
                    {
                        account.Balance -= amount;
                        var receiveraccount = this.bank.AccountsList.SingleOrDefault(m => m.AccountNumber == receiveraccnumber);
                        receiveraccount.Balance += amount;
                        AddTransaction(senderaccnumber,"WithDraw", DateTime.Now, amount);
                        AddTransaction(receiveraccnumber, "deposit", DateTime.Now, amount);


                    }
                    else
                        throw new Exception("Insufficient balance");
                    
                }

                else
                {
                    throw new Exception("Invalid pin number");
                }
            }
            return true;


        }
        public double remBalance(int accountnumber, int pin)
        {

            var account = this.bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);

            if (account is null)
            {
                throw new Exception("Either invalid");
            }
            else
            {
                if (account.Password == pin)
                    return account.Balance;

                else
                    throw new Exception("Invalid pin number");
            }
        }
        public void AddTransaction(int targetid, string type, DateTime datetime, double amount)
        {
            var account = this.bank.AccountsList.SingleOrDefault(m => m.AccountNumber == targetid);
            Transaction transaction = new(targetid, type, amount, datetime.ToString());
            account.Transactions.Add(transaction);
        }
        public void GetTransactionhistory(int acnumber)
        {
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == acnumber);
            if (account is null)
            {
                Console.WriteLine("account number is invalid");
            }
            else
            {
                foreach (Transaction transaction in account.Transactions)
                {
                    Console.WriteLine(transaction.Datetime + " " + transaction.Type);
                    if (transaction.Type == "deposit")
                        Console.WriteLine("credited+" + transaction.Amount);
                    else
                        Console.WriteLine("debited- " + transaction.Amount);
                    Console.WriteLine("");

                }
            }

        }
    }

}


