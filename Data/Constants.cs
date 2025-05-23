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
        internal const string CommandMustBeAStringError = "Command must be a string.";
        internal const string AmountMustBeANumberError = "Amount must be a number.";
        internal const string ExitCommand = "Exit";
        
        internal const decimal MinBetAmount = 1;

        internal enum Actions
        {
            Deposit = 1,
            Bet = 2,
            Withdraw = 3,
            Exit = 4
        }
    }
}
