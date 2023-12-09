using System;
using System.Collections.Generic;

public class Transaction
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

public class Card
{
    private string type;
    private string details;

    public Card(string type, string details)
    {
        this.type = type;
        this.details = details;
    }

    public override string ToString()
    {
        return $"| {type,-15} | {details,-40} |";
    }
}

public class CardManager
{
    private List<Card> cards = new List<Card>();

    public void AddCard(string type, string details)
    {
        Card card = new Card(type, details);
        cards.Add(card);
        Console.WriteLine("Card added successfully.");
    }

    public void DisplayCards()
    {
        if (cards.Count == 0)
        {
            Console.WriteLine("No cards available.");
        }
        else
        {
            Console.WriteLine("Card Manager:");
            Console.WriteLine("+-----------------+----------------------------------------+");
            Console.WriteLine("| Type            | Details                                |");
            Console.WriteLine("+-----------------+----------------------------------------+");

            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine("+-----------------+----------------------------------------+");
        }
    }

    public void RemoveCard(int index)
    {
        if (index >= 0 && index < cards.Count)
        {
            cards.RemoveAt(index);
            Console.WriteLine("Card removed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid card index.");
        }
    }
}

public class Wallet
{
    private List<Transaction> transactionHistory = new List<Transaction>();
    private double balance = 0.0;
    private double balanceEuro = 0.0;
    private double balanceDollar = 0.0;
    private CardManager cardManager = new CardManager();

    // Conversion rates
    private readonly double conversionRateToDollar = 0.00022;
    private readonly double conversionRateToEuro = 0.00020;

    public void Deposit(double amount)
    {
        balance += amount;
        AddTransaction(amount, "Deposit");
        Console.WriteLine("Deposit successful. Current balance: " + balance);
    }

    public void DepositDollar(double amount)
    {
        balanceDollar += amount;
        AddTransaction(amount, "depositDollar");
        Console.WriteLine("Deposit successful. Current balance: " + balanceDollar);
    }

    public void DepositEuro(double amount)
    {
        balanceEuro += amount;
        AddTransaction(amount, "depositEuro");
        Console.WriteLine("Deposit successful. Current balance: " + balanceEuro);
    }

    public void MakePayment(double amount)
    {
        if (balance >= amount)
        {
            balance -= amount;
            AddTransaction(-amount, "Payment");
            Console.WriteLine("Payment successful. Current balance: " + balance);
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
        Console.WriteLine("Conversion successful. Dollar amount: " + convertedAmount);
    }

    public void ConvertToEuro(double amount)
    {
        double convertedAmount = amount * conversionRateToEuro;
        balance -= amount;
        AddTransaction(-amount, "Ariary to Euro Conversion");
        DepositEuro(convertedAmount);
        Console.WriteLine("Conversion successful. Euro amount: " + convertedAmount);
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
        Console.WriteLine("Current Balance: " + balance);
        Console.WriteLine("Current BalanceDollar: " + balanceDollar);
        Console.WriteLine("Current BalanceEuro: " + balanceEuro);
    }

    public void CardManagerMenu()
    {
        while (true)
        {
            Console.WriteLine(@"
                    Card Manager Menu:
                    1 - Add Card
                    2 - Display Cards
                    3 - Remove Card
                    0 - Back to Main Menu
                    ");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    AddCard();
                    break;
                case 2:
                    cardManager.DisplayCards();
                    break;
                case 3:
                    RemoveCard();
                    break;
                case 0:
                    Console.WriteLine("Returning to the main menu.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void AddCard()
    {
        Console.WriteLine("Enter card type (e.g., National ID, Driver's License): ");
        string type = Console.ReadLine();
        Console.WriteLine("Enter card details: ");
        string details = Console.ReadLine();
        cardManager.AddCard(type, details);
    }

    private void RemoveCard()
    {
        Console.WriteLine("Enter the index of the card to remove: ");
        int index = int.Parse(Console.ReadLine());
        cardManager.RemoveCard(index);
    }

    public static void Main(string[] args)
    {
        Wallet wallet = new Wallet();

        while (true)
        {
            Console.WriteLine(@"
                    Welcome to your Wallet, what I can do for You master!!
                    Choose an option:
                    1 - Deposit
                    2 - Make Payment
                    3 - Convert to Dollar
                    4 - Convert to Euro
                    5 - Display Transaction History
                    6 - Display Current Balance
                    7 - Card Manager
                    0 - Exit
                    ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter deposit amount: ");
                    double depositAmount = double.Parse(Console.ReadLine());
                    wallet.Deposit(depositAmount);
                    break;
                case 2:
                    Console.Write("Enter payment amount: ");
                    double paymentAmount = double.Parse(Console.ReadLine());
                    wallet.MakePayment(paymentAmount);
                    break;
                case 3:
                    Console.Write("Enter Ariary amount to convert to Dollar: ");
                    double ariaryToDollarAmount = double.Parse(Console.ReadLine());
                    wallet.ConvertToDollar(ariaryToDollarAmount);
                    break;
                case 4:
                    Console.Write("Enter Ariary amount to convert to Euro: ");
                    double ariaryToEuroAmount = double.Parse(Console.ReadLine());
                    wallet.ConvertToEuro(ariaryToEuroAmount);
                    break;
                case 5:
                    wallet.DisplayTransactionHistory();
                    break;
                case 6:
                    wallet.DisplayBalance();
                    break;
                case 7:
                    wallet.CardManagerMenu();
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
