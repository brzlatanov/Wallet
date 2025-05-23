using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Interfaces;

namespace Wallet.Controllers
{
    internal class GameController
    {
        IView view;
        public GameController(IView view)
        {
            this.view = view;
        }
        public void Run()
        {
            view.RenderView();
            string input = view.ReceiveInput();
        }
    }
}
