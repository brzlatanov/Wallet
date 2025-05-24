using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data;
using Wallet.Interfaces;

namespace Wallet.UI
{
    internal class ConsoleView : IView
    {
        public void RenderView(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
        }
    }
}
