using System;
using System.Collections.Generic;
using System.Linq;
using BankApp;
using BankApp.Models;
using BankApp.Models.Exceptions;

namespace BankApp.Service
{
    public class BankService
    {        
        static int AccountNumber = 12345;
        static int BId = 1;
        private List<Bank> banks;
        public BankService()
            {
            this.banks=new  List<Bank>();
            }
        public int addBank(string bankname)
        {
             Bank bank = new Bank
             {
                 BankId = BId,
                 BankName = bankname
             };
            this.banks.Add(bank);
            BId++;
            return bank.BankId;
         }
        public int createAccount(int bankId, string AccountName, int password, double initialbal)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            Account account = new Account(AccountNumber, AccountName, password, initialbal);
            bank.AccountsList.Add(account);
            AccountNumber++;
            return account.AccountNumber;
        }
        public bool Deposit(int bankId,double amount, int accountnumber, int pin)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
            if (bank is null)
                throw new IncorrectBankIdException();
            if (account is null)
                throw new IncorrectAccountNumberException();          
            if (account.Password == pin)
            {
                account.Balance += amount;
                TransactionType.transactionType t = (TransactionType.transactionType)1;
                Transaction transaction = new Transaction(accountnumber,amount,t,DateTime.Now);
                account.Transactions.Add(transaction);
            }
            else
                throw new IncorrectPin();
            return true;
        }
        public bool WithDraw(int bankId,double amount, int accountnumber, int pin)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
            if (account is null)
                throw new IncorrectAccountNumberException();
            else
            {
                if (account.Password == pin)
                {
                    if (account.Balance < amount)
                        throw new AmountNotSufficient();
                    else
                    {

                        account.Balance -= amount;
                        TransactionType.transactionType t = (TransactionType.transactionType)2;
                        Transaction transaction = new Transaction(accountnumber, amount, t, DateTime.Now);
                        account.Transactions.Add(transaction);
                    }
                }
                else
                    throw new IncorrectPin();
            }
            return true;

        } 
        public bool transferAmount(int senderbankId, int senderaccnumber, int pin, double amount, int receiverbankid,int receiveraccnumber)
        {
            Bank senderbank = this.banks.SingleOrDefault(m => m.BankId == senderbankId);
            if (senderbank is null)
                throw new IncorrectBankIdException();
            var senderaccount = senderbank.AccountsList.SingleOrDefault(m => m.AccountNumber == senderaccnumber);
            if (senderaccount is null)
                throw new Exception("Account invalid");
            if (senderaccount.Password == pin)
            {
                if (senderaccount.Balance > amount)
                {
                    Bank receiverbank = this.banks.SingleOrDefault(m => m.BankId == receiverbankid);
                    if (receiverbank is null)
                        throw new IncorrectBankIdException();
                    var receiveraccount = receiverbank.AccountsList.SingleOrDefault(m => m.AccountNumber == receiveraccnumber);
                    if (receiveraccount is null)
                        throw new Exception("Account invalid");
                    senderaccount.Balance -= amount;
                    receiveraccount.Balance += amount;
                    TransactionType.transactionType t = (TransactionType.transactionType)1;
                    Transaction transaction = new Transaction(senderaccnumber, amount, t, DateTime.Now);
                    senderaccount.Transactions.Add(transaction);
                    TransactionType.transactionType t1 = (TransactionType.transactionType)2;
                    Transaction transaction1 = new Transaction(receiveraccnumber, amount, t1, DateTime.Now);
                    receiveraccount.Transactions.Add(transaction1);
                }
                else
                    throw new AmountNotSufficient();
            }
            else
                throw new IncorrectPin();
            return true;
        }
        public double GetBalance(int BankId,int accountnumber, int pin)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == BankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
            if (account is null)
                throw new IncorrectAccountNumberException();
            else
            {
                if (account.Password == pin)
                    return account.Balance;

                else
                    throw new Exception("Invalid pin number");
            }
        }
        public List<Transaction>  GetTransactionhistory(int bankId,int acnumber,int password)
        {
            var bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == acnumber);
            if (account is null)
                throw new IncorrectAccountNumberException();
            if (account.Password == password)
                return account.Transactions;
            else
                throw new IncorrectPin();
        }
    }

}


