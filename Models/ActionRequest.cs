using System.ComponentModel.DataAnnotations;

namespace Wallet.Models
{
    public class ActionRequest
    {
        [Required(ErrorMessage = "Command is required.")]
        [RegularExpression("^(bet|deposit|withdraw|exit)$", ErrorMessage = "Invalid command.")]
        public string Command { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Amount must be between 1 and 10.")]
        public decimal Amount { get; set; }
    }
}
