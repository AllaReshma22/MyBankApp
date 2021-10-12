using System;
using BankApp.Services;
using BankApp.Models;
using System.Linq;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Giving title to the console
            Console.Title = "Bank App";
            //displaying options
            printString("______________________________");
            printString("|ENTER YOUR CHOICE           |");
            printString("|1.Create account            |");
            printString("|2.Deposit Amount            |");
            printString("|3.Withdraw amount           |");
            printString("|4.transfer amount           |");
            printString("|5.checkbalance              |");
            printString("|6.Transaction history       |");
            printString("|7.Exit                      |");
            printString("______________________________");
            BankServices bankService = new BankServices();
            bool i = true;
            while (i)
            {

                Features features = (Features)Enum.Parse(typeof(Features), Console.ReadLine());

                switch (features)
                {
                    case Features.CreateAccount:
                        {
                            string acname = GetUserInput("Enter account holder name");
                            int password= GetUserInputAsInt("Enter pin for setup");
                            double balance = GetUserInputAsDouble("Enter initial balance");
                            bankService.createAccount(acname, password, balance);
                            printString($"Account created succesfully in bank with accountnumber {BankServices.accountNumber - 1}");
                            break;

                        }
                    case Features.Deposit:
                        {
                            double amount = GetUserInputAsDouble("Enter amount");
                            int AccountNumber = GetUserInputAsInt("Enter account number");
                            int password = GetUserInputAsInt("Enter pin");
                            try
                            {
                                bankService.Deposit(amount, AccountNumber, password);
                                printString($"Amount{amount} deposited in accountnumber{AccountNumber}");
                            }
                            catch (Exception ex)
                            {
                                printString(ex.Message);
                            }

                            break;
                        }
                    case Features.Withdraw:
                        {
                            double amount = GetUserInputAsDouble("Enter amount");
                            int accountnumber= GetUserInputAsInt("Enter Ac number");
                            int password = GetUserInputAsInt("Enter pin: ");
                            try
                            {
                                bankService.WithDraw(amount, accountnumber, password);
                                printString($"Amount{amount}withdrawn from accountnumber{accountnumber}");
                            }
                            catch (Exception ex)
                            {
                                printString(ex.Message);
                            }
                            break;
                        }
                    case Features.Checkbalance:
                        {
                            int accountnumber= GetUserInputAsInt("enter acnumber");
                            int password = GetUserInputAsInt("Enter pin");
                            try
                            {
                                printString($"{bankService.remBalance(accountnumber, password)}");
                            }
                            catch (Exception ex)
                            {
                                printString(ex.Message);
                            }

                            break;
                        }
                    case Features.TransferAmount:
                        {
                            int seacnumber = GetUserInputAsInt("entersender account number");
                            int spassword = GetUserInputAsInt("Enter pin");
                            int reacnumber = GetUserInputAsInt("enter receiver account number");
                            double amount = GetUserInputAsDouble("Enter amount");
                            try
                            {
                                bankService.transferAmount(amount, seacnumber, spassword, reacnumber);
                                printString("Amount transferred succesfully");
                            }
                            catch (Exception ex)
                            {
                                printString(ex.Message);
                            }
                            break;
                        }

                    case Features.Transactionhistory:
                        {
                            int acnumber = GetUserInputAsInt("enter acnumber");
                            bankService.GetTransactionhistory(acnumber);
                            break;
                        }


                    case Features.Exit:
                        {
                            i = false;
                            break;
                        }


                }
            }


        }
        public enum Features
        {
            CreateAccount = 1,
            Deposit,
            Withdraw,
            TransferAmount,
            Checkbalance,
            Transactionhistory,
            Exit,
        }
        static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        
        static int GetUserInputAsInt(string message)
        {
            Console.WriteLine(message);
            return int.Parse(Console.ReadLine());
        }
        static double GetUserInputAsDouble(string message)
        {
            Console.WriteLine(message);
            return double.Parse(Console.ReadLine());
        }

        public static int printString(string str)
        {
            Console.WriteLine(str);
            return 1;
        }


    }
}
