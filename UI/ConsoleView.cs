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
        public void RenderView()
        {
            Console.WriteLine(Constants.SubmitActionPrompt);
        }

        public string ReceiveInput()
        {
            string action = Console.ReadLine();

            return action;
        }
    }
}
