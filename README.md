# Wallet Project - C# Version

This project implements a simple wallet application in C# with additional functionality for managing cards (e.g., National ID, Driver's License, Credit Card, Business Card). The project includes features like deposit, make payment, currency conversion, transaction history, and a card manager.

## Functionalities

1. **Deposit**: Add funds to your wallet balance.
2. **Make Payment**: Deduct funds from your wallet balance for a payment.
3. **Currency Conversion**:
    - Convert Ariary to Dollar.
    - Convert Ariary to Euro.
4. **Display Transaction History**: View a history of all transactions.
5. **Display Current Balance**: Check the current balance in Ariary, Dollar, and Euro.
6. **Card Manager**:
    - Add a card.
    - Display all cards.
    - Remove a card.

## Setup and Launching C# Code

### Prerequisites

1. **Install .NET SDK**:

   You need the .NET SDK installed on your machine to compile and run C# code. Follow these steps to install the .NET SDK:

    - Visit the [.NET SDK download page](https://dotnet.microsoft.com/download).
    - Download and install the SDK for your operating system.

2. **Download the Project**:

   Clone or download the C# branch of the project from the repository.

   ```bash
   git clone -b feature/c# https://github.com/hei-school/cc-d1-my-wallet-miharyjoe.git
   
   cd wallet
   
   dotnet run wallet.cs