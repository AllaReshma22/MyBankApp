using System;
using BankApp.Service;
using BankApp.Models;
using System.Linq;
using System.Collections.Generic;
using BankApp.Models.Exceptions;

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
            printString("|1.Add Bank                    |");
            printString("|2.Create account            |");
            printString("|3.Deposit Amount            |");
            printString("|4.Withdraw amount           |");
            printString("|5.transfer amount           |");
            printString("|6.checkbalance              |");
            printString("|7.Transaction history       |");
            printString("|8.Exit                      |");
            printString("______________________________");
            BankService bankService = new BankService();
            UserMenu userMenu = new UserMenu();
            while (true)
            {

                UserMenu.Features features = (UserMenu.Features)Enum.Parse(typeof(UserMenu.Features), Console.ReadLine());

                switch (features)
                {
                    case UserMenu.Features.AddBank:
                        {
                            String Bankname = GetUserInput("Enter bank name");
                            int bankid = bankService.addBank(Bankname);
                            printString($"Bank added successfully with bank id{bankid} and bankname {Bankname}");
                            break;
                        }
                    case UserMenu.Features.CreateAccount:
                        {
                            int bankid = GetUserInputAsInt("Enter the bankid ");
                            string AccountHolderName = GetUserInput("Enter account holder name");
                            int Password = GetUserInputAsInt("Enter password for setup:");
                            double Balance = GetUserInputAsDouble("Enter initial balance");
                            try
                            {
                                int AccountNumber = bankService.createAccount(bankid, AccountHolderName, Password, Balance);
                                printString($"Account created succesfully in bank with accountnumber {AccountNumber} in bank with bankid {bankid}");
                            }
                            catch (IncorrectBankIdException ex)
                            {
                                printString(ex.Message);
                            }

                            break;

                        }
                    case UserMenu.Features.Deposit:
                        {
                            int bankid = GetUserInputAsInt("Enter the bankid ");
                            int AccountNumber = GetUserInputAsInt("Enter account number");
                            double Amount = GetUserInputAsDouble("Enter amount");
                            int Password = GetUserInputAsInt("Enter password");
                            try
                            {
                                bankService.Deposit(bankid,Amount, AccountNumber, Password);
                                printString($"Amount{Amount} deposited in accountnumber {AccountNumber}");
                            }
                            catch (AmountNotSufficient ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectBankIdException ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectPin ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectAccountNumberException ex)
                            {
                                printString(ex.Message);
                            }
                            break;
                        }
                  case UserMenu.Features.Withdraw:
                        {
                            int bankid = GetUserInputAsInt("Enter the bankid ");
                            int AccountNumber = GetUserInputAsInt("Enter Ac number");
                            double Amount = GetUserInputAsDouble("Enter amount");
                            int Password = GetUserInputAsInt("Enter pin: ");
                            try
                            {
                                bankService.WithDraw(bankid,Amount, AccountNumber, Password);
                                printString($"Amount{Amount}withdrawn from accountnumber{AccountNumber}");
                            }
                            catch (AmountNotSufficient ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectBankIdException ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectPin ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectAccountNumberException ex)
                            {
                                printString(ex.Message);
                            }
                            break;
                        }
              
                    case UserMenu.Features.CheckBalance:
                        {
                            int bankid = GetUserInputAsInt("Enter the bankid ");
                            int AccountNumber = GetUserInputAsInt("enter acnumber");
                            int Password = GetUserInputAsInt("Enter pin");
                            try
                            {
                                printString($"{bankService.GetBalance(bankid,AccountNumber, Password)}");
                            }
                            catch (AmountNotSufficient ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectBankIdException ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectPin ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectAccountNumberException ex)
                            {
                                printString(ex.Message);
                            }
                            break;
                        }
                   case UserMenu.Features.TransferAmount:
                        {
                            int senderbankid = GetUserInputAsInt("Enter the bankid ");
                            int SenderAccountNumber = GetUserInputAsInt("entersender account number");
                            int SenderPassword = GetUserInputAsInt("Enter pin");
                            int receiverbankid = GetUserInputAsInt("Enter the receiver bankid");
                            int ReceiverAccountNumber = GetUserInputAsInt("enter receiver account number");
                            double Amount = GetUserInputAsDouble("Enter amount");
                            try
                            {
                                bankService.transferAmount(senderbankid,SenderAccountNumber, SenderPassword,Amount,receiverbankid, ReceiverAccountNumber);
                                printString("Amount transferred succesfully");
                            }
                            catch(AmountNotSufficient ex)
                            {
                                printString(ex.Message);
                            }
                            catch(IncorrectBankIdException ex)
                            {
                                printString(ex.Message);
                            }
                            catch(IncorrectPin ex)
                            {
                                printString(ex.Message);
                            }
                            catch(IncorrectAccountNumberException ex)
                            {
                                printString(ex.Message);
                            }
                            break;
                        }

                    case UserMenu.Features.Transactionhistory:
                        {
                            int bankid = GetUserInputAsInt("Enter the bankid ");
                            int AccountNumber = GetUserInputAsInt("enter acnumber");
                            int Password = GetUserInputAsInt("Enter Password");
                            try
                            {
                                List<Transaction> transactions = bankService.GetTransactionhistory(bankid, AccountNumber, Password);
                                foreach (Transaction transaction in transactions)
                                {
                                    printString(transaction.dateTime + " " + transaction.Type);
                                    if (transaction.Type == TransactionType.transactionType.Deposit)
                                        printString("credited+" + transaction.Amount);
                                    else
                                        printString("debited- " + transaction.Amount);
                                    printString("");
                                }
                            }
                            catch (IncorrectBankIdException ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectPin ex)
                            {
                                printString(ex.Message);
                            }
                            catch (IncorrectAccountNumberException ex)
                            {
                                printString(ex.Message);
                            }


                            break;
                        }
            

                    case UserMenu.Features.Exit:
                        {
                            Environment.Exit(0);
                            break;
                        }


                }
            }


        }
        static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static int GetUserInputAsInt(string message)
        {
            return int.Parse(GetUserInput(message));
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
