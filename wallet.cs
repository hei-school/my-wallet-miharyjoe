using System;
using System.Collections.Generic;

class Transaction
{
    private DateTime date;
    private double amount;
    private string status;

    public Transaction(double amount, string status)
    {
        this.date = DateTime.Now;
        this.amount = amount;
        this.status = status;
    }

    public override string ToString()
    {
        return $"| {date.ToString("yyyy-MM-dd HH:mm:ss"),-20} | {amount,-10:F2} | {status,-15} |";
    }
}

class Wallet
{
    private List<Transaction> transactionHistory = new List<Transaction>();
    private double balance = 0.0;
    private double balanceEuro = 0.0;
    private double balanceDollar = 0.0;

    // The conversion rate is not a fixed value but changes every day
    private readonly double conversionRateToDollar = 0.00022;
    private readonly double conversionRateToEuro = 0.00020;

    public void Deposit(double amount)
    {
        balance += amount;
        AddTransaction(amount, "Deposit");
        Console.WriteLine($"Deposit successful. Current balance: {balance}");
    }

    public void DepositDollar(double amount)
    {
        balanceDollar += amount;
        AddTransaction(amount, "Deposit Dollar");
        Console.WriteLine($"Deposit successful. Current balance: {balanceDollar}");
    }

    public void DepositEuro(double amount)
    {
        balanceEuro += amount;
        AddTransaction(amount, "Deposit Euro");
        Console.WriteLine($"Deposit successful. Current balance: {balanceEuro}");
    }

    public void MakePayment(double amount)
    {
        if (balance >= amount)
        {
            balance -= amount;
            AddTransaction(-amount, "Payment");
            Console.WriteLine($"Payment successful. Current balance: {balance}");
        }
        else
        {
            Console.WriteLine("Insufficient funds. Payment failed.");
        }
    }

    public void ConvertToDollar(double amount)
    {
        double convertedAmount = amount * conversionRateToDollar;
        balance -= amount;
        AddTransaction(-amount, "Ariary to Dollar Conversion");
        DepositDollar(convertedAmount);
        Console.WriteLine($"Conversion successful. Dollar amount: {convertedAmount}");
    }

    public void ConvertToEuro(double amount)
    {
        double convertedAmount = amount * conversionRateToEuro;
        balance -= amount;
        AddTransaction(-amount, "Ariary to Euro Conversion");
        DepositEuro(convertedAmount);
        Console.WriteLine($"Conversion successful. Euro amount: {convertedAmount}");
    }

    public void DisplayTransactionHistory()
    {
        Console.WriteLine("Transaction History:");
        Console.WriteLine("+---------------------+------------+-----------------+");
        Console.WriteLine("| Date                | Amount     | Status          |");
        Console.WriteLine("+---------------------+------------+-----------------+");

        foreach (Transaction transaction in transactionHistory)
        {
            Console.WriteLine(transaction);
        }

        Console.WriteLine("+---------------------+------------+-----------------+");
    }

    private void AddTransaction(double amount, string status)
    {
        Transaction transaction = new Transaction(amount, status);
        transactionHistory.Add(transaction);
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Current Balance: {balance}");
        Console.WriteLine($"Current Balance Dollar: {balanceDollar}");
        Console.WriteLine($"Current Balance Euro: {balanceEuro}");
    }

    static void Main()
    {
        Wallet wallet = new Wallet();

        while (true)
        {
            Console.WriteLine(@"
                    Welcome to your Wallet, what can I do for you master!!
                    Choose an option:
                    1 - Deposit
                    2 - Make Payment
                    3 - Convert to Dollar
                    4 - Convert to Euro
                    5 - Display Transaction History
                    6 - Display Current Balance
                    0 - Exit
                ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter deposit amount: ");
                    double depositAmount;
                    if (!double.TryParse(Console.ReadLine(), out depositAmount))
                    {
                        Console.WriteLine("Invalid input for deposit amount.");
                        continue;
                    }
                    wallet.Deposit(depositAmount);
                    break;
                case 2:
                    Console.Write("Enter payment amount: ");
                    double paymentAmount;
                    if (!double.TryParse(Console.ReadLine(), out paymentAmount))
                    {
                        Console.WriteLine("Invalid input for payment amount.");
                        continue;
                    }
                    wallet.MakePayment(paymentAmount);
                    break;
                case 3:
                    Console.Write("Enter Ariary amount to convert to Dollar: ");
                    double ariaryToDollarAmount;
                    if (!double.TryParse(Console.ReadLine(), out ariaryToDollarAmount))
                    {
                        Console.WriteLine("Invalid input for conversion amount.");
                        continue;
                    }
                    wallet.ConvertToDollar(ariaryToDollarAmount);
                    break;
                case 4:
                    Console.Write("Enter Ariary amount to convert to Euro: ");
                    double ariaryToEuroAmount;
                    if (!double.TryParse(Console.ReadLine(), out ariaryToEuroAmount))
                    {
                        Console.WriteLine("Invalid input for conversion amount.");
                        continue;
                    }
                    wallet.ConvertToEuro(ariaryToEuroAmount);
                    break;
                case 5:
                    wallet.DisplayTransactionHistory();
                    break;
                case 6:
                    wallet.DisplayBalance();
                    break;
                case 0:
                    Console.WriteLine("Exiting. Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
