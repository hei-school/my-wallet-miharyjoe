from datetime import datetime

class Transaction:
    def __init__(self, amount, status):
        self.date = datetime.now()
        self.amount = amount
        self.status = status

    def __str__(self):
        date_string = self.date.strftime("%Y-%m-%d %H:%M:%S")
        return f"| {date_string} | {self.amount:.2f} | {self.status.ljust(15)} |"

class Card:
    def __init__(self, type, details):
        self.type = type
        self.details = details

    def __str__(self):
        return f"| {self.type.ljust(15)} | {self.details.ljust(40)} |"

class CardManager:
    def __init__(self):
        self.cards = []

    def add_card(self, type, details):
        card = Card(type, details)
        self.cards.append(card)
        print("Card added successfully.")

    def display_cards(self):
        if not self.cards:
            print("No cards available.")
        else:
            print("Card Manager:")
            print("+-----------------+----------------------------------------+")
            print("| Type            | Details                                |")
            print("+-----------------+----------------------------------------+")
            for card in self.cards:
                print(card)
            print("+-----------------+----------------------------------------+")

    def remove_card(self, index):
        if 0 <= index < len(self.cards):
            del self.cards[index]
            print("Card removed successfully.")
        else:
            print("Invalid card index.")

class Wallet:
    def __init__(self):
        self.transaction_history = []
        self.balance = 0.0
        self.balance_euro = 0.0
        self.balance_dollar = 0.0
        self.card_manager = CardManager()
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
            print(transaction)
        print("+---------------------+------------+-----------------+")

    def add_transaction(self, amount, status):
        transaction = Transaction(amount, status)
        self.transaction_history.append(transaction)

    def display_balance(self):
        print(f"Current Balance: {self.balance}")
        print(f"Current Balance Dollar: {self.balance_dollar}")
        print(f"Current Balance Euro: {self.balance_euro}")

    def card_manager_menu(self):
        while True:
            print("""
                    Card Manager Menu:
                    1 - Add Card
                    2 - Display Cards
                    3 - Remove Card
                    0 - Back to Main Menu
                    """)

            choice = int(input())
            if choice == 1:
                self.add_card()
            elif choice == 2:
                self.card_manager.display_cards()
            elif choice == 3:
                self.remove_card()
            elif choice == 0:
                print("Returning to the main menu.")
                return
            else:
                print("Invalid choice. Please try again.")

    def add_card(self):
        input()  # Consume the newline character
        type = input("Enter card type (e.g., National ID, Driver's License): ")
        details = input("Enter card details: ")
        self.card_manager.add_card(type, details)

    def remove_card(self):
        index = int(input("Enter the index of the card to remove: "))
        self.card_manager.remove_card(index)

    def run(self):
        while True:
            print("""
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
                    """)

            choice = int(input())
            if choice == 1:
                deposit_amount = float(input("Enter deposit amount: "))
                self.deposit(deposit_amount)
            elif choice == 2:
                payment_amount = float(input("Enter payment amount: "))
                self.make_payment(payment_amount)
            elif choice == 3:
                ariary_to_dollar_amount = float(input("Enter Ariary amount to convert to Dollar: "))
                self.convert_to_dollar(ariary_to_dollar_amount)
            elif choice == 4:
                ariary_to_euro_amount = float(input("Enter Ariary amount to convert to Euro: "))
                self.convert_to_euro(ariary_to_euro_amount)
            elif choice == 5:
                self.display_transaction_history()
            elif choice == 6:
                self.display_balance()
            elif choice == 7:
                self.card_manager_menu()
            elif choice == 0:
                print("Exiting. Goodbye!")
                break
            else:
                print("Invalid choice. Please try again.")


if __name__ == "__main__":
    wallet = Wallet()
    wallet.run()
