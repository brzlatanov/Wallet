using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Services
{
    public interface IBettingOrchestrationService
    {
        Task<string> PlaceBetAsync(decimal amount);
    }
}
