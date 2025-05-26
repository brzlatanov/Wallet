using Wallet.Interfaces;
using Wallet.Shared;

namespace Wallet.Validators
{
    public class Validator : IValidator
    {
        public string ValidateInput(string value)
        {
            var parts = value.Trim().Split(' ');

            if (parts.Length != 2)
            {
                return Constants.InvalidActionError;
            }
            else if (!decimal.TryParse(parts[1], out var amount))
            {
                return Constants.AmountMustBeANumberError;
            }
            else if(amount <= 0)
            {
                return Constants.AmountMustBePositiveError;
            }

            return "";
        }
    }
}
