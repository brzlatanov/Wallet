using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Interfaces
{
    public interface IValidator
    {
        string ValidateInput(string input);
    }
}
