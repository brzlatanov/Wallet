using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Helpers
{
    public static class FormatHelper
    {
        public const string precision = "{0:C2}"; 

        public static string FormatAmount(object amount)
        {
            return string.Format("{0:C2}", amount);
        }

        public static string FormatMessage(string message, decimal amount, decimal newBalance)
        {
            return string.Format(message, FormatAmount(amount), FormatAmount(newBalance));
        }

        public static string FormatMessage(string message, decimal amount, string newBalance)
        {
            return string.Format(message, FormatAmount(amount), FormatAmount(newBalance));
        }

        public static string FormatMessage(string message, decimal amount)
        {
            return string.Format(message, FormatAmount(amount));
        }

        public static string FormatMessage(string message, string arguments)
        {
            return string.Format(message, arguments);
        }
    }
}
