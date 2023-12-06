from datetime import datetime

class Transaction:
    def __init__(self, amount, status):
        self.date = datetime.now()
        self.amount = amount
        self.status = status

    def __str__(self):
        date_str = self.date.strftime("%Y-%m-%d %H:%M:%S")
        return f"| {date_str} | {self.amount:.2f} | {self.status} |"

class Wallet:
    def __init__(self):
        self.transaction_history = []
        self.balance = 0.0
        self.balance_dollar = 0.0
        self.balance_euro = 0.0
        self.conversion_rate_to_dollar = 0.00022
        self.conversion_rate_to_euro = 0.00020

    def deposit(self, amount):
        self.balance += amount
        self.add_transaction(amount, "Deposit")
        print(f"Deposit successful. Current balance: {self.balance}")

    def deposit_dollar(self, amount):
        self.balance_dollar += amount
        self.add_transaction(amount, "Deposit Dollar")
        print(f"Deposit successful. Current balance: {self.balance_dollar}")

    def deposit_euro(self, amount):
        self.balance_euro += amount
        self.add_transaction(amount, "Deposit Euro")
        print(f"Deposit successful. Current balance: {self.balance_euro}")

    def make_payment(self, amount):
        if self.balance >= amount:
            self.balance -= amount
            self.add_transaction(-amount, "Payment")
            print(f"Payment successful. Current balance: {self.balance}")
        else:
            print("Insufficient funds. Payment failed.")

    def convert_to_dollar(self, amount):
        converted_amount = amount * self.conversion_rate_to_dollar
        self.balance -= amount
        self.add_transaction(-amount, "Ariary to Dollar Conversion")
        self.deposit_dollar(converted_amount)
        print(f"Conversion successful. Dollar amount: {converted_amount}")

    def convert_to_euro(self, amount):
        converted_amount = amount * self.conversion_rate_to_euro
        self.balance -= amount
        self.add_transaction(-amount, "Ariary to Euro Conversion")
        self.deposit_euro(converted_amount)
        print(f"Conversion successful. Euro amount: {converted_amount}")

    def display_transaction_history(self):
        print("Transaction History:")
        print("+---------------------+------------+-----------------+")
        print("| Date                | Amount     | Status          |")
        print("+---------------------+------------+-----------------+")

        for transaction in self.transaction_history:
            print(str(transaction))

        print("+---------------------+------------+-----------------+")

    def add_transaction(self, amount, status):
        transaction = Transaction(amount, status)
        self.transaction_history.append(transaction)

    def display_balance(self):
        print(f"Current Balance: {self.balance}")
        print(f"Current Balance Dollar: {self.balance_dollar}")
        print(f"Current Balance Euro: {self.balance_euro}")

if __name__ == "__main__":
    wallet = Wallet()

    while True:
        print("""
            Welcome to your Wallet, what can I do for you master!!
            Choose an option:
            1 - Deposit
            2 - Make Payment
            3 - Convert to Dollar
            4 - Convert to Euro
            5 - Display Transaction History
            6 - Display Current Balance
            0 - Exit
        """)

        choice = int(input("Enter your choice: "))

        if choice == 1:
            deposit_amount = float(input("Enter deposit amount: "))
            wallet.deposit(deposit_amount)
        elif choice == 2:
            payment_amount = float(input("Enter payment amount: "))
            wallet.make_payment(payment_amount)
        elif choice == 3:
            ariary_to_dollar_amount = float(input("Enter Ariary amount to convert to Dollar: "))
            wallet.convert_to_dollar(ariary_to_dollar_amount)
        elif choice == 4:
            ariary_to_euro_amount = float(input("Enter Ariary amount to convert to Euro: "))
            wallet.convert_to_euro(ariary_to_euro_amount)
        elif choice == 5:
            wallet.display_transaction_history()
        elif choice == 6:
            wallet.display_balance()
        elif choice == 0:
            print("Exiting. Goodbye!")
            break
        else:
            print("Invalid choice. Please try again.")
