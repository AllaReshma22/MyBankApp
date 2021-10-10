using System;
using BankApp.Services;
using BankApp.Models;
namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
            {
                //Giving title to the console
                Console.Title = "ATM Machine";
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
            while(true)
            {

                Features features = (Features)Enum.Parse(typeof(Features), Console.ReadLine());

                switch (features)
                { 
                    case Features.CreateAccount:
                        {
                            printString("Enter account holder name");
                            string acname = getUserInput();
                            printString("Enter pin for setup");
                            int pin = getUserInputAsInt();
                            printString("Enter initial balance");
                            int balance = getUserInputAsInt();
                            bankService.createAccount(acname, pin, balance);
                            printString($"Account created succesfully in bank ");
                            break;

                        }
                    case Features.Deposit:
                        {
                            printString("Enter amount");
                            int amount = getUserInputAsInt();
                            printString("Enter Ac number");
                            int Acnumber = getUserInputAsInt();
                            printString("Enter pin");
                            int pin = getUserInputAsInt();
                            try
                            {
                                bankService.Deposit(amount, Acnumber, pin);
                                printString($"Amount{amount} deposited in accountnumber{Acnumber}");
                            }
                            catch(Exception ex)
                            {
                                printString(ex.Message);
                            }
                         
                            break;
                        }
                    case Features.Withdraw:
                        {
                            printString("Enter amount");
                            int amount = getUserInputAsInt();
                            
                            printString("Enter Ac number");
                            int Acnumber = getUserInputAsInt();
                            printString("Enter pin:");
                            int pin = getUserInputAsInt();
                            try
                            {
                                bankService.WithDraw(amount, Acnumber, pin);
                                printString($"Amount{amount}withdrawn from accountnumber{Acnumber}");
                            }
                            catch(Exception ex)
                            {
                                printString(ex.Message);
                            }
                            break;
                        }
                    case Features.checkbalance:
                        {
                            printString("enter acnumber");
                            int acnumber = getUserInputAsInt();
                            printString("Enter pin");
                            int pin = getUserInputAsInt();
                            try
                            {
                                printString($"{bankService.remBalance(acnumber, pin)}");
                            }
                            catch(Exception ex)
                            {
                                printString(ex.Message);
                            }

                            break;
                        }
                    case Features.TransferAmount:
                        {
                            printString("entersender account number");
                            int seacnumber = getUserInputAsInt();
                            printString("Enter pin");
                            int spin = getUserInputAsInt();
                            printString("enter receiver account number");
                            int reacnumber = getUserInputAsInt();
                            printString("Enter amount");
                            int amount = getUserInputAsInt();
                            try
                            {
                                bankService.transferAmount(amount, seacnumber, spin, reacnumber);
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
                            printString("enter acnumber");
                            int acnumber = getUserInputAsInt();
                            foreach (Transaction transaction in bankService.transaclist)
                            {
                                if (transaction.toid == acnumber)
                                {
                                    printString(transaction.Datetime + " " + transaction.Type);
                                    if (transaction.Type == "deposit")
                                        printString("credited+" + transaction.Amount);
                                    else
                                        printString("debited- " + transaction.Amount);
                                    printString("");

                                }
                            }
                                break;
                        }


                    case Features.Exit:
                        {
                            break;
                        }


                }
            } 


            }
        public enum Features
        {
            CreateAccount=1,
            Deposit,
            Withdraw,
            TransferAmount,
            checkbalance,
            Transactionhistory,
            Exit,
        }
        static string getUserInput()
        {
            return Console.ReadLine();
        }
        static int getUserInputAsInt()
        {
            return int.Parse(Console.ReadLine());
        }
        public static int printString(string str)
        {
            Console.WriteLine(str);
            return 1;
        }


    }
}
    