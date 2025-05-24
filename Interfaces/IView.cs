using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Wallet.Interfaces
{
    internal interface IView
    {
        void RenderView(string message);
    }
}
