using System;
using System.Collections.Generic;

namespace BankAccounts
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, BankAccount> bankAccounts = new Dictionary<int, BankAccount>(5);
            string userInput = "";

            do
            {
                Console.WriteLine("1. New account\n2. List all accounts\n3. Exit\n\n");
                Console.Write("Enter your choice: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddAccount(bankAccounts);
                        break;
                    case "2":
                        DisplayAccounts(bankAccounts);
                        break;
                    default:
                        break;
                }

                Console.Clear();

            } while (userInput != "3");
        }

        static void AddAccount(Dictionary<int, BankAccount> bankAccounts)
        {
            BankAccount newAccount = new BankAccount(GetAccountNumber(bankAccounts), 0, GetAccountType());

            bankAccounts.Add(newAccount.Id, newAccount);
        }

        static int GetAccountNumber(Dictionary<int, BankAccount> bankAccounts)
        {
            int newAccountId;
            Random randomId = new Random();

            do
            {
                newAccountId = randomId.Next(1, int.MaxValue);
            } while (CheckIfExists(newAccountId, bankAccounts));

            return newAccountId;
        }

        static bool CheckIfExists(int newId, Dictionary<int, BankAccount> bankAccounts)
        {
            if (bankAccounts.ContainsKey(newId))
            {
                return true;
            }
            return false;
        }

        static AccountType GetAccountType()
        {
            string userInput = "";

            while (true)
            {
                Console.Clear();

                Console.WriteLine($"1. {AccountType.Savings}\n2. {AccountType.Giro}\n3. {AccountType.Current}");

                Console.Write("\n\nChoose account type: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        return AccountType.Savings;
                    case "2":
                        return AccountType.Giro;
                    case "3":
                        return AccountType.Current;
                    default:
                        break;
                }
            }
        }

        static void DisplayAccounts(Dictionary<int, BankAccount> bankAccounts)
        {
            Console.Clear();

            if (bankAccounts.Count == 0)
            {
                Console.WriteLine("There are no bank accounts to display.");
            }
            else
            {
                foreach (var account in bankAccounts)
                {
                    Console.WriteLine($"Account ID: {account.Value.Id}");
                    Console.WriteLine($"Account balance: {account.Value.Balance}");
                    Console.WriteLine($"Account type: {account.Value.Type}\n");
                }
            }

            Console.Write("\n\nPress any key to go back...");
            Console.ReadKey();
        }
    }
}
