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
                return this.ValidationErrors;
            }

            var parts = value.Trim().Split(' ');

            if (parts.Length != 2)
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

            return this.ValidationErrors;
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

            return this.ValidationErrors;
        }

        public void AddValidationError(string error)
        {
            if (!this.ValidationErrors.Contains(error))
            {
                this.ValidationErrors.Add(error);
            }
        }
    }
}
