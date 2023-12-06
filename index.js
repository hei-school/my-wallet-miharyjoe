const readline = require("readline");

class Transaction {
  constructor(amount, status) {
    this.date = new Date();
    this.amount = amount;
    this.status = status;
  }

  toString() {
    const dateString = this.date.toISOString().slice(0, 19).replace("T", " ");
    return `| ${dateString} | ${this.amount.toFixed(2)} | ${this.status} |`;
  }
}

class Wallet {
  constructor() {
    this.transactionHistory = [];
    this.balance = 0.0;
    this.balanceDollar = 0.0;
    this.balanceEuro = 0.0;
    this.conversionRateToDollar = 0.00022;
    this.conversionRateToEuro = 0.0002;
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
    this.addTransaction(amount, "Deposit Dollar");
    console.log(`Deposit successful. Current balance: ${this.balanceDollar}`);
  }

  depositEuro(amount) {
    this.balanceEuro += amount;
    this.addTransaction(amount, "Deposit Euro");
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
    const convertedAmount = amount * this.conversionRateToDollar;
    this.balance -= amount;
    this.addTransaction(-amount, "Ariary to Dollar Conversion");
    this.depositDollar(convertedAmount);
    console.log(`Conversion successful. Dollar amount: ${convertedAmount}`);
  }

  convertToEuro(amount) {
    const convertedAmount = amount * this.conversionRateToEuro;
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

  getUserInput(question) {
    return new Promise((resolve) => {
      this.rl.question(question, (answer) => {
        resolve(answer);
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
