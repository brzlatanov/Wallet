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

        internal enum Actions
        {
            Deposit = 1,
            Bet = 2,
            Withdraw = 3,
            Exit = 4
        }
    }
}
