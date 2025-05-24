using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Helpers
{
    internal static class FormatHelper
    {
        internal const string precision = "{0:C2}"; 

        internal static string FormatAmount(decimal amount)
        {
            return String.Format("{0:C2}", amount);
        }

        internal static string FormatMessage(string message, decimal amount, decimal newBalance)
        {
            return String.Format(message, FormatAmount(amount), FormatAmount(newBalance));
        }

        internal static string FormatMessage(string message, decimal newBalance)
        {
            return String.Format(message, FormatAmount(newBalance));
        }

        internal static string FormatMessage(string message, string arguments)
        {
            return String.Format(message, arguments);
        }
    }
}
