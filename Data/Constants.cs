using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Data
{
    internal static class Constants
    {
        internal const string SubmitActionPrompt = "Please, submit action:";
        internal const string InvalidActionError = "Invalid command format. Use: <command> <amount>";
        internal const string InvalidCommandError = "Invalid command. Available commands: {0}";
        internal const string CommandMustBeAStringError = "Command must be a string.";
        internal const string AmountMustBeANumberError = "Amount must be a number.";
        internal const string AmountMustBePositiveError = "Amount must be positive.";
        internal const string AmountMustBeWithinRangeError = "Amount must be between {0} and {1}.";
        internal const string BetLostMessage = "No luck this time! Your current balance is: {0}";
        internal const string BetWonMessage = "Congrats - you won {0}! Your current balance is: {1}";
        internal const string ExitCommand = "Exit";
        internal const string GoodbyeMessage = "Thank you for playing! Hope to see you again soon!";
        internal const string SuccessfulDepositMessage = "Your deposit of {0} was successful. Your new balance is: {1}";
        internal const string SuccessfulWithdrawalMessage = "Your withdrawal of {0} was successful. Your new balance is: {1}";
        internal const string InsufficientFundsMessage = "Insufficient funds for withdrawal. Current balance: {0}";

        internal const decimal MinBetAmount = 1;
        internal const decimal MaxBetAmount = 10;

        internal static class Actions
        {
            public const string Deposit = "deposit";
            public const string Bet = "bet";
            public const string Withdraw = "withdraw";
            public const string Exit = "exit";

            public static readonly List<string> All = new() { Deposit, Bet, Withdraw, Exit };
        }
    }
}
