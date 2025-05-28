using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class ErrorDTO
    {
        [Required]
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
