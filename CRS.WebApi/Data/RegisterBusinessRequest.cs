using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CRS.WebApi.Data
{
    public class RegisterBusinessRequest
    {
        [Required]
        [JsonPropertyName("businessName")]
        public required string BusinessName { get; set; }

    }
}
