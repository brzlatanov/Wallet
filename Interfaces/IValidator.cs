using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Interfaces
{
    internal interface IValidator
    {
        IEnumerable<string> ValidateInput(string input);

        void AddValidationError(string error);
    }
}
