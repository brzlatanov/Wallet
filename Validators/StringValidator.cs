using Wallet.Interfaces;
using Wallet.Data;

namespace Wallet.Validators
{
    internal class StringValidator : IValidator
    {
        List<string> ValidationErrors = new();
        public IEnumerable<string> ValidateInput(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AddValidationError(Constants.InvalidActionError);
            }

            var parts = value.Split(' ');

            if (parts == null || parts.Length != 2)
            {
                AddValidationError(Constants.InvalidActionError);
            }
            else if (!decimal.TryParse(parts[1], out var amount))
            {
                AddValidationError(Constants.AmountMustBeANumberError);
            }

            return ValidationErrors;
        }

        public void AddValidationError(string error)
        {
            if (!ValidationErrors.Contains(error))
            {
                ValidationErrors.Add(error);
            }
        }
    }
}
