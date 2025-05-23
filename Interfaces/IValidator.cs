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
        IEnumerable<string> ValidateObject(object obj);
        void AddValidationError(string error);
    }
}
