import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Scanner;

class Transaction {
  private Date date;
  private double amount;
  private String status;

  public Transaction(double amount, String status) {
    this.date = new Date();
    this.amount = amount;
    this.status = status;
  }

  @Override
  public String toString() {
    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
    return String.format("| %-20s | %-10.2f | %-15s |", sdf.format(date), amount, status);
  }
}

class Card {
  private String type;
  private String details;

  public Card(String type, String details) {
    this.type = type;
    this.details = details;
  }

  @Override
  public String toString() {
    return String.format("| %-15s | %-40s |", type, details);
  }
}

class CardManager {
  private List<Card> cards = new ArrayList<>();

  public void addCard(String type, String details) {
    Card card = new Card(type, details);
    cards.add(card);
    System.out.println("Card added successfully.");
  }

  public void displayCards() {
    if (cards.isEmpty()) {
      System.out.println("No cards available.");
    } else {
      System.out.println("Card Manager:");
      System.out.println("+-----------------+----------------------------------------+");
      System.out.println("| Type            | Details                                |");
      System.out.println("+-----------------+----------------------------------------+");

      for (Card card : cards) {
        System.out.println(card);
      }

      System.out.println("+-----------------+----------------------------------------+");
    }
  }

  public void removeCard(int index) {
    if (index >= 0 && index < cards.size()) {
      cards.remove(index);
      System.out.println("Card removed successfully.");
    } else {
      System.out.println("Invalid card index.");
    }
  }
}

public class Wallet {
  private List<Transaction> transactionHistory = new ArrayList<>();
  private Scanner scanner = new Scanner(System.in);
  private double balance = 0.0;
  private double balanceEuro = 0.0;
  private double balanceDollar = 0.0;
  private CardManager cardManager = new CardManager();

  // Conversion rates
  private final double conversionRateToDollar = 0.00022;
  private final double conversionRateToEuro = 0.00020;

  public void deposit(double amount) {
    balance += amount;
    addTransaction(amount, "Deposit");
    System.out.println("Deposit successful. Current balance: " + balance);
  }

  public void depositDollar(double amount) {
    balanceDollar += amount;
    addTransaction(amount, "depositDollar");
    System.out.println("Deposit successful. Current balance: " + balanceDollar);
  }

  public void depositEuro(double amount) {
    balanceEuro += amount;
    addTransaction(amount, "depositEuro");
    System.out.println("Deposit successful. Current balance: " + balanceEuro);
  }

  public void makePayment(double amount) {
    if (balance >= amount) {
      balance -= amount;
      addTransaction(-amount, "Payment");
      System.out.println("Payment successful. Current balance: " + balance);
    } else {
      System.out.println("Insufficient funds. Payment failed.");
    }
  }

  public void convertToDollar(double amount) {
    double convertedAmount = amount * conversionRateToDollar;
    balance -= amount;
    addTransaction(-amount, "Ariary to Dollar Conversion");
    depositDollar(convertedAmount);
    System.out.println("Conversion successful. Dollar amount: " + convertedAmount);
  }

  public void convertToEuro(double amount) {
    double convertedAmount = amount * conversionRateToEuro;
    balance -= amount;
    addTransaction(-amount, "Ariary to Euro Conversion");
    depositEuro(convertedAmount);
    System.out.println("Conversion successful. Euro amount: " + convertedAmount);
  }

  public void displayTransactionHistory() {
    System.out.println("Transaction History:");
    System.out.println("+---------------------+------------+-----------------+");
    System.out.println("| Date                | Amount     | Status          |");
    System.out.println("+---------------------+------------+-----------------+");

    for (Transaction transaction : transactionHistory) {
      System.out.println(transaction);
    }

    System.out.println("+---------------------+------------+-----------------+");
  }

  private void addTransaction(double amount, String status) {
    Transaction transaction = new Transaction(amount, status);
    transactionHistory.add(transaction);
  }

  public void displayBalance() {
    System.out.println("Current Balance: " + balance);
    System.out.println("Current BalanceDollar: " + balanceDollar);
    System.out.println("Current BalanceEuro: " + balanceEuro);
  }

  public void cardManagerMenu() {
    while (true) {
      System.out.println("""
                    Card Manager Menu:
                    1 - Add Card
                    2 - Display Cards
                    3 - Remove Card
                    0 - Back to Main Menu
                    """);

      int choice = scanner.nextInt();
      switch (choice) {
        case 1:
          addCard();
          break;
        case 2:
          cardManager.displayCards();
          break;
        case 3:
          removeCard();
          break;
        case 0:
          System.out.println("Returning to the main menu.");
          return;
        default:
          System.out.println("Invalid choice. Please try again.");
      }
    }
  }

  private void addCard() {
    scanner.nextLine(); // Consume the newline character
    System.out.println("Enter card type (e.g., National ID, Driver's License): ");
    String type = scanner.nextLine();
    System.out.println("Enter card details: ");
    String details = scanner.nextLine();
    cardManager.addCard(type, details);
  }

  private void removeCard() {
    System.out.println("Enter the index of the card to remove: ");
    int index = scanner.nextInt();
    cardManager.removeCard(index);
  }

  public static void main(String[] args) {
    Wallet wallet = new Wallet();
    Scanner scanner = new Scanner(System.in);

    while (true) {
      System.out.println("""
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
                    """);

      int choice = scanner.nextInt();

      switch (choice) {
        case 1:
          System.out.print("Enter deposit amount: ");
          double depositAmount = scanner.nextDouble();
          wallet.deposit(depositAmount);
          break;
        case 2:
          System.out.print("Enter payment amount: ");
          double paymentAmount = scanner.nextDouble();
          wallet.makePayment(paymentAmount);
          break;
        case 3:
          System.out.print("Enter Ariary amount to convert to Dollar: ");
          double ariaryToDollarAmount = scanner.nextDouble();
          wallet.convertToDollar(ariaryToDollarAmount);
          break;
        case 4:
          System.out.print("Enter Ariary amount to convert to Euro: ");
          double ariaryToEuroAmount = scanner.nextDouble();
          wallet.convertToEuro(ariaryToEuroAmount);
          break;
        case 5:
          wallet.displayTransactionHistory();
          break;
        case 6:
          wallet.displayBalance();
          break;
        case 7:
          wallet.cardManagerMenu();
          break;
        case 0:
          System.out.println("Exiting. Goodbye!");
          System.exit(0);
          break;
        default:
          System.out.println("Invalid choice. Please try again.");
      }
    }
  }
}
