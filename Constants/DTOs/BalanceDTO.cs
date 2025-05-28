using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class BalanceDTO
    {
        [Required]
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
