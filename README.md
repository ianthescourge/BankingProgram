# BankingProgram
Simple banking program in C# by Ian Sanford.

Performs the basic workflows:

-Create a new account
-Login
-Record a deposit
-Record a withdrawal
-Check balance
-See transaction history
-Log out

Does not use persistant storage. User accounts, balances and transaction histories are stored until the program is terminated. 
Performs basic input checking. 
Deposits and withdrawls accept fractions of a cent but when viewing balace the total is rounded down to the nearest cent.
You can choose to login while already logged in, and it will simply sign you into the chosen account (if it exists).
Trying to deposit, withdrawl, check balance, see transaction history or logout while not logged in will simply display an error message,
and the program will continue.
