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
        public void RenderView(IEnumerable<string>? messages)
        {
            if (messages?.Any() == true)
            {
                foreach (var message in messages)
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
