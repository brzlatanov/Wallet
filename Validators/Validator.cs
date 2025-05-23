using System.ComponentModel.DataAnnotations;
using Wallet.Data;
using Wallet.Interfaces;
using Wallet.Models;

namespace Wallet.Validators
{
    internal class Validator : IValidator
    {
        List<string> ValidationErrors;
        public IEnumerable<string> ValidateInput(string value)
        {
            ValidationErrors = new();

            if (string.IsNullOrWhiteSpace(value))
            {
                AddValidationError(Constants.InvalidActionError);
            }

            var parts = value.Trim().Split(' ');

            if (parts == null || parts.Length != 2)
            {
                AddValidationError(Constants.InvalidActionError);
            }
            else if (!decimal.TryParse(parts[1], out var amount))
            {
                AddValidationError(Constants.AmountMustBeANumberError);
            }
            else if(amount <= 0)
            {
                AddValidationError(Constants.AmountMustBePositiveError);
            }

            return ValidationErrors;
        }

        public IEnumerable<string> ValidateObject(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            bool isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    AddValidationError(validationResult.ErrorMessage);
                }
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
