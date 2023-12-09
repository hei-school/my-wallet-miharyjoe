const readline = require("readline");

class Transaction {
  constructor(amount, status) {
    this.date = new Date();
    this.amount = amount;
    this.status = status;
  }

  toString() {
    const sdf = new Intl.DateTimeFormat("en", {
      year: "numeric",
      month: "2-digit",
      day: "2-digit",
      hour: "2-digit",
      minute: "2-digit",
      second: "2-digit",
    });

    const dateString = sdf.format(this.date);
    return `| ${dateString} | ${this.amount.toFixed(2)} | ${this.status} |`;
  }
}

class Card {
  constructor(type, details) {
    this.type = type;
    this.details = details;
  }

  toString() {
    return `| ${this.type.padEnd(15)} | ${this.details.padEnd(40)} |`;
  }
}

class CardManager {
  constructor() {
    this.cards = [];
  }

  addCard(type, details) {
    const card = new Card(type, details);
    this.cards.push(card);
    console.log("Card added successfully.");
  }

  displayCards() {
    if (this.cards.length === 0) {
      console.log("No cards available.");
    } else {
      console.log("Card Manager:");
      console.log(
        "+-----------------+----------------------------------------+"
      );
      console.log(
        "| Type            | Details                                |"
      );
      console.log(
        "+-----------------+----------------------------------------+"
      );

      for (const card of this.cards) {
        console.log(card.toString());
      }

      console.log(
        "+-----------------+----------------------------------------+"
      );
    }
  }

  removeCard(index) {
    if (index >= 0 && index < this.cards.length) {
      this.cards.splice(index, 1);
      console.log("Card removed successfully.");
    } else {
      console.log("Invalid card index.");
    }
  }
}

class Wallet {
  constructor() {
    this.transactionHistory = [];
    this.balance = 0.0;
    this.balanceEuro = 0.0;
    this.balanceDollar = 0.0;
    this.cardManager = new CardManager();
    this.rl = readline.createInterface({
      input: process.stdin,
      output: process.stdout,
    });
  }

  deposit(amount) {
    this.balance += amount;
    this.addTransaction(amount, "Deposit");
    console.log(`Deposit successful. Current balance: ${this.balance}`);
  }

  depositDollar(amount) {
    this.balanceDollar += amount;
    this.addTransaction(amount, "depositDollar");
    console.log(`Deposit successful. Current balance: ${this.balanceDollar}`);
  }

  depositEuro(amount) {
    this.balanceEuro += amount;
    this.addTransaction(amount, "depositEuro");
    console.log(`Deposit successful. Current balance: ${this.balanceEuro}`);
  }

  makePayment(amount) {
    if (this.balance >= amount) {
      this.balance -= amount;
      this.addTransaction(-amount, "Payment");
      console.log(`Payment successful. Current balance: ${this.balance}`);
    } else {
      console.log("Insufficient funds. Payment failed.");
    }
  }

  convertToDollar(amount) {
    const convertedAmount = amount * 0.00022;
    this.balance -= amount;
    this.addTransaction(-amount, "Ariary to Dollar Conversion");
    this.depositDollar(convertedAmount);
    console.log(`Conversion successful. Dollar amount: ${convertedAmount}`);
  }

  convertToEuro(amount) {
    const convertedAmount = amount * 0.0002;
    this.balance -= amount;
    this.addTransaction(-amount, "Ariary to Euro Conversion");
    this.depositEuro(convertedAmount);
    console.log(`Conversion successful. Euro amount: ${convertedAmount}`);
  }

  displayTransactionHistory() {
    console.log("Transaction History:");
    console.log("+---------------------+------------+-----------------+");
    console.log("| Date                | Amount     | Status          |");
    console.log("+---------------------+------------+-----------------+");

    for (const transaction of this.transactionHistory) {
      console.log(transaction.toString());
    }

    console.log("+---------------------+------------+-----------------+");
  }

  addTransaction(amount, status) {
    const transaction = new Transaction(amount, status);
    this.transactionHistory.push(transaction);
  }

  displayBalance() {
    console.log(`Current Balance: ${this.balance}`);
    console.log(`Current Balance Dollar: ${this.balanceDollar}`);
    console.log(`Current Balance Euro: ${this.balanceEuro}`);
  }

  async cardManagerMenu() {
    while (true) {
      console.log(`
                    Card Manager Menu:
                    1 - Add Card
                    2 - Display Cards
                    3 - Remove Card
                    0 - Back to Main Menu
                `);

      const choice = await this.getUserInput("Enter your choice: ");

      switch (parseInt(choice)) {
        case 1:
          await this.addCard();
          break;
        case 2:
          this.cardManager.displayCards();
          break;
        case 3:
          await this.removeCard();
          break;
        case 0:
          console.log("Returning to the main menu.");
          return;
        default:
          console.log("Invalid choice. Please try again.");
      }
    }
  }

  async addCard() {
    console.log("Enter card type (e.g., National ID, Driver's License): ");
    const type = await this.getUserInput("");
    console.log("Enter card details: ");
    const details = await this.getUserInput("");
    this.cardManager.addCard(type, details);
  }

  async removeCard() {
    console.log("Enter the index of the card to remove: ");
    const index = await this.getUserInput("");
    this.cardManager.removeCard(parseInt(index));
  }

  getUserInput(question) {
    return new Promise((resolve) => {
      this.rl.question(question, (answer) => {
        resolve(answer.trim());
      });
    });
  }

  async run() {
    while (true) {
      console.log(`
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
            `);

      const choice = await this.getUserInput("Enter your choice: ");

      switch (parseInt(choice)) {
        case 1:
          const depositAmount = parseFloat(
            await this.getUserInput("Enter deposit amount: ")
          );
          this.deposit(depositAmount);
          break;
        case 2:
          const paymentAmount = parseFloat(
            await this.getUserInput("Enter payment amount: ")
          );
          this.makePayment(paymentAmount);
          break;
        case 3:
          const ariaryToDollarAmount = parseFloat(
            await this.getUserInput(
              "Enter Ariary amount to convert to Dollar: "
            )
          );
          this.convertToDollar(ariaryToDollarAmount);
          break;
        case 4:
          const ariaryToEuroAmount = parseFloat(
            await this.getUserInput("Enter Ariary amount to convert to Euro: ")
          );
          this.convertToEuro(ariaryToEuroAmount);
          break;
        case 5:
          this.displayTransactionHistory();
          break;
        case 6:
          this.displayBalance();
          break;
        case 7:
          await this.cardManagerMenu();
          break;
        case 0:
          console.log("Exiting. Goodbye!");
          this.rl.close();
          process.exit(0);
          break;
        default:
          console.log("Invalid choice. Please try again.");
      }
    }
  }
}

const wallet = new Wallet();
wallet.run();
