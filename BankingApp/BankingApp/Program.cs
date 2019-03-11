using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Banking program by Ian Sanford.
  Simple banking program with accounts and transactions stored locally. 
  I have included rudimentary input checking on most fields.
  Bank account totals can deposit and withdrawl fractions of a cent,
  but when displaying the balance is rounded down to the nearest cent.*/

namespace BankingApp
{
    //Holds transaction information for an account
    public class Transaction
    {
        private readonly string transactionType;
        private readonly decimal amount;
        private readonly DateTime dateTime;
        public Transaction(string type, decimal dollarAmount)
        {
            this.transactionType = type;
            this.amount = dollarAmount;
            this.dateTime = DateTime.Now;
        }
        public string GetTransactionType()
        {
            return transactionType;
        }
        public decimal GetAmount()
        {
            return amount;
        }
        public DateTime GetDateTime()
        {
            return dateTime;
        }
    }

    //holds information related to an account, such as username, password, balance and transaction history
    public class Account
    {

        private readonly string username;
        private readonly string password;
        private decimal balance;
        List<Transaction> transactionList = new List<Transaction>();
        public Account(string user, string pass)
        {
            this.username = user;
            this.password = pass;
            this.balance = 0;
        }
        public string GetPassword()
        {
            return password;
        }
        public string GetUsername()
        {
            return username;
        }
        public decimal GetBalance()
        {
            return balance;
        }
        public void MakeDeposit(decimal deposit)
        {
            balance += deposit;
            transactionList.Add(new Transaction("Deposit", deposit));
        }
        public void MakeWithdrawl(decimal withdrawl)
        {
            balance -= withdrawl;
            transactionList.Add(new Transaction("Withdrawl", withdrawl));
        }
        public List<Transaction> GetTransactions()
        {
            return transactionList;
        }
    }

    //Handles account creation. Checks new usernames are not in use and not empty or "void"
    //does not perform checking on passwords
    public class AccountCreator
    {
        private string userName;
        private string password;
        List<Account> accounts;

        public AccountCreator(ref List<Account> accountListRef)
        {
            this.accounts = accountListRef;
            GetUserName();
            GetPassword();
        }

        private void writeBanner()
        {
            Console.Clear();
            Console.WriteLine("Welcome To bank, give us ur moneys \n");
            Console.WriteLine("Create a new account");
            Console.WriteLine("--------------------\n");
        }

        private bool uniqueUsername(string username)
        {
            bool unique = true;
            foreach (Account acc in accounts)
            {
                if (username == acc.GetUsername())
                {
                    unique = false;
                }
            }
            return unique;
        }

        private void GetUserName()
        {
            bool userNameSelected = false;
            while (userNameSelected == false)
            {
                writeBanner();
                Console.Write("Please enter a username: ");
                string tempUserName = Console.ReadLine();
                Console.WriteLine("\nYou entered " + tempUserName + ". Is this correct? Y/N");
                char response = Console.ReadKey().Key.ToString()[0];
                if (response == 'Y')
                {
                    if (uniqueUsername(tempUserName) && tempUserName != "void" && tempUserName != "")
                    {
                        userName = tempUserName;
                        writeBanner();
                        Console.WriteLine("Your New Username is: " + userName + "\n");
                        Console.WriteLine("Press any key to continue.");
                        userNameSelected = true;
                        Console.ReadKey();
                    }
                    else
                    {
                        writeBanner();
                        Console.WriteLine("I'm sorry, the username \"" + tempUserName + "\" is already taken. Please choose another.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    writeBanner();
                    Console.WriteLine("\nResetting Username. Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        private void GetPassword()
        {
            bool passwordSelected = false;
            while (passwordSelected == false)
            {
                writeBanner();
                Console.Write("Please enter a Password: ");
                string tempPassword = Console.ReadLine();
                Console.WriteLine("\nYou entered " + tempPassword + ". Is this correct? Y/N");
                char response = Console.ReadKey().Key.ToString()[0];
                if (response == 'Y')
                {
                    password = tempPassword;
                    passwordSelected = true;
                    writeBanner();
                    Console.WriteLine("Congratulations! Thank you for starting an account with us!\n");
                    Console.WriteLine("Your Username is: " + userName);
                    Console.WriteLine("Your Password is: " + password + "\n");
                    Console.WriteLine("Press any key to return to the main selection screen.");
                    accounts.Add(new Account(userName, password));
                    Console.ReadKey();
                }
                else
                {
                    writeBanner();
                    Console.WriteLine("\nResetting Password. Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }
    }

    //Handles login. Checks username and password against account list.
    class LoginManager
    {
        private readonly List<Account> accountList;
        public LoginManager(List<Account> accounts)
        {
            this.accountList = accounts;
        }
        //returns an account with username:"void" and password:"void" if can't find username/password combo and user elects not to retry.
        public Account LoginPortal(ref bool loggedIn)
        {
            bool usernameEntered = false;
            string tempUserName = "error";
            string tempPassword = "";
            bool loginSuccess = false;
            while (!loginSuccess)
            {
                while (!usernameEntered)
                {
                    writeBanner();
                    Console.Write("Please enter your username: ");
                    tempUserName = Console.ReadLine();
                    Console.WriteLine("\nYou entered " + tempUserName + ". Is this correct? Y/N");
                    char response = Console.ReadKey().Key.ToString()[0];
                    if (response == 'Y')
                    {
                        usernameEntered = true;
                    }
                    else
                    {
                        writeBanner();
                        Console.WriteLine("\nResetting Username. Press any key to continue.");
                        Console.ReadKey();
                    }
                }

                writeBanner();
                Console.WriteLine("Username : " + tempUserName);
                Console.Write("Password : ");
                tempPassword = Console.ReadLine();
                foreach (Account account in accountList)
                {
                    if (account.GetUsername() == tempUserName && account.GetPassword() == tempPassword)
                    {
                        loginSuccess = true;
                        Console.WriteLine("\nWelcome " + tempUserName + "! You are now logged in. Press any key to return to the main menu.");
                        loggedIn = true;
                        Console.ReadKey();

                    }
                }
                if (!loginSuccess)
                {
                    usernameEntered = false;
                    Console.WriteLine("\nUsername and/or Password not recognized.");
                    Console.WriteLine("Press 'Y' to retry, or press any other key to return to main menu.");
                    char response = Console.ReadKey().Key.ToString()[0];
                    if (response != 'Y')
                    {
                        return new Account("void", "void");
                    }
                }
            }
            return new Account(tempUserName, tempPassword);
        }

        private void writeBanner()
        {
            Console.Clear();
            Console.WriteLine("Welcome To bank, give us ur moneys \n");
            Console.WriteLine("Login Portal");
            Console.WriteLine("--------------------\n");
        }
    }

    //Handles displaying the balance of accounts, making deposits/withdrawls, and displaying the transaction history.
    class AccountManager
    {
        private void writeBanner()
        {
            Console.Clear();
            Console.WriteLine("Welcome To bank, give us ur moneys \n");
            Console.WriteLine("Account Information");
            Console.WriteLine("--------------------\n");
        }

        public void DisplayBalance(Account userAccount, ref List<Account> accountList)
        {
            writeBanner();
            foreach (Account account in accountList)
            {
                if (userAccount.GetUsername() == account.GetUsername())
                {
                    decimal roundedDown = Math.Floor(account.GetBalance() * 100) / 100;
                    string formattedBalance = String.Format("{0:0,0.00}", roundedDown);
                    Console.WriteLine("Username: " + account.GetUsername());
                    Console.WriteLine("Balance : " + formattedBalance);
                }
            }
            Console.WriteLine("\nPress any key to return to main menu.");
            Console.ReadKey();
        }

        public void MakeDeposit(Account userAccount, ref List<Account> accountList)
        {
            bool depositCompleted = false;
            while (!depositCompleted)
            {
                string depositAmountString;
                writeBanner();
                Console.Write("\nPlease enter an amount to deposit: ");
                depositAmountString = Console.ReadLine();
                bool isNumber = true;
                foreach (char c in depositAmountString)
                {
                    if (!Char.IsDigit(c) && c != '.')
                    {
                        isNumber = false;
                    }
                }
                if (isNumber)
                {
                    foreach (Account account in accountList)
                    {
                        if (userAccount.GetUsername() == account.GetUsername())
                        {
                            account.MakeDeposit(decimal.Parse(depositAmountString));
                            Console.WriteLine("\nDeposit Successful. Press any key to continue.");
                            depositCompleted = true;
                        }
                    }
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nInvalid entry. Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void MakeWithdrawl(Account userAccount, ref List<Account> accountList)
        {
            string withdrawlAmountString;
            writeBanner();
            foreach (Account account in accountList)
            {
                if (userAccount.GetUsername() == account.GetUsername())
                {
                    decimal roundedDown = Math.Floor(account.GetBalance() * 100) / 100;
                    string formattedBalance = String.Format("{0:0,0.00}", roundedDown);
                    Console.WriteLine("Your current balance is: " + formattedBalance);
                    Console.Write("Please enter an amount to withdrawl: ");
                    withdrawlAmountString = Console.ReadLine();
                    bool isNumber = true;
                    foreach (char c in withdrawlAmountString)
                    {
                        if (!Char.IsDigit(c) && c != '.')
                        {
                            isNumber = false;
                        }
                    }
                    if (isNumber)
                    {
                        decimal withdrawlAmount = decimal.Parse(withdrawlAmountString);
                        if (withdrawlAmount <= account.GetBalance())
                        {
                            account.MakeWithdrawl(withdrawlAmount);
                            Console.WriteLine("\nWithdrawl Successful. Press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("\nError! Withdrawl amount cannot exceed current balance.");
                            Console.ReadKey();
                        }
                    }
                }
            }

        }

        public void DisplayTransactionHistory(Account userAccount, ref List<Account> accountList)
        {
            writeBanner();
            foreach (Account account in accountList)
            {
                if (userAccount.GetUsername() == account.GetUsername())
                {
                    List<Transaction> transactionList = account.GetTransactions();
                    Console.WriteLine("Your transaction history for account: " + account.GetUsername());
                    Console.WriteLine("-------------------------------------\n");
                    foreach (Transaction transaction in transactionList)
                    {
                        Console.WriteLine($"Type: {transaction.GetTransactionType(),-10} Amount: {transaction.GetAmount(),-10}  Time: {transaction.GetDateTime(),-15}");
                    }
                    Console.WriteLine("\nEnd of transaction history. Press any key to return to the main menu.");
                    Console.ReadKey();
                }
            }
        }
    }

    //Displays the main menu screen and returns a valid selection of a menu item.
    //Also will indicate that the user is logged in.
    class MainMenu
    {
        bool m_loggedIn;
        public int GetSelection(bool loggedIn, ref Account account)
        {
            m_loggedIn = loggedIn;
            int selection = 0;
            bool validSelection = false;
            while (!validSelection)
            {
                Console.Clear();
                Console.WriteLine("Welcome To Bank. Give us all your moneys. \n");
                Console.WriteLine("---------------------------------- \n");
                if (m_loggedIn)
                {
                    Console.WriteLine("Logged in as: " + account.GetUsername() + "\n");
                }
                Console.WriteLine("Please Select from the following:");
                Console.WriteLine("   1. Create Account");
                Console.WriteLine("   2. Login");
                Console.WriteLine("   3. Record a Deposit");
                Console.WriteLine("   4. Record a Withdrawl");
                Console.WriteLine("   5. Check Balance");
                Console.WriteLine("   6. See Transaction History");
                Console.WriteLine("   7. Log Out");
                Console.WriteLine("   8. Exit\n");
                Console.WriteLine("Please enter a number from 1-8 ");
                string tempSelection = Console.ReadLine();
                if (Int32.TryParse(tempSelection, out int tempInt))
                {
                    if (tempInt > 0 && tempInt < 9)
                    {
                        selection = tempInt;
                        validSelection = true;
                    }
                    else
                    {
                        Console.WriteLine("\nIncorrect Selection, Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("\nIncorrect Selection, Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            return selection;
        }
    }

    //Starting point for the program. Handles menu selections and contains the account list.
    //Will continue to loop until 'exit' is selected.
    class Start
    {
        static List<Account> accountList = new List<Account>();
        static bool loggedIn = false;

        static void displayMustBeLoggedIn()
        {
            Console.WriteLine("\nI'm sorry, but you must be logged in to complete this action.");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {

            MainMenu menu = new MainMenu();
            bool exit = false;
            LoginManager loginManager = new LoginManager(accountList);
            AccountManager accountManager = new AccountManager();
            Account accountLoggedIn = new Account("void", "void");
            while (!exit)
            {
                int selection = menu.GetSelection(loggedIn, ref accountLoggedIn);
                switch (selection)
                {
                    case 1:
                        AccountCreator ac = new AccountCreator(ref accountList);
                        break;
                    case 2:
                        accountLoggedIn = loginManager.LoginPortal(ref loggedIn);
                        break;
                    case 3:
                        if (loggedIn)
                        {
                            accountManager.MakeDeposit(accountLoggedIn, ref accountList);
                        }
                        else
                        {
                            displayMustBeLoggedIn();
                        }
                        break;
                    case 4:
                        if (loggedIn)
                        {
                            accountManager.MakeWithdrawl(accountLoggedIn, ref accountList);
                        }
                        else
                        {
                            displayMustBeLoggedIn();
                        }
                        break;
                    case 5:
                        if (loggedIn)
                        {
                            accountManager.DisplayBalance(accountLoggedIn, ref accountList);
                        }
                        else
                        {
                            displayMustBeLoggedIn();
                        }
                        break;
                    case 6:
                        if (loggedIn)
                        {
                            accountManager.DisplayTransactionHistory(accountLoggedIn, ref accountList);
                        }
                        else
                        {
                            displayMustBeLoggedIn();
                        }
                        break;
                    case 7:
                        accountLoggedIn = new Account("void", "void");
                        loggedIn = false;
                        Console.WriteLine("\nYou have logged out. Press any key to continue.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nThank you for banking with us today!");
                        Console.ReadKey();
                        exit = true;
                        break;
                }
            }
        }
    }
}
