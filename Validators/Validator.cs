using System.ComponentModel.DataAnnotations;
using Wallet.Data;
using Wallet.Interfaces;
using Wallet.Models;

namespace Wallet.Validators
{
    internal class Validator : IValidator
    {
        public string ValidateInput(string value)
        {
            //var error = String.Empty;

            //if (string.IsNullOrWhiteSpace(value))
            //{
            //    error = Constants.InvalidActionError;
            //}

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
