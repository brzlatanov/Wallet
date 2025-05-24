using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Interfaces
{
    internal interface IBettingService
    {
        decimal PlaceBet(decimal amount);
    }
}
