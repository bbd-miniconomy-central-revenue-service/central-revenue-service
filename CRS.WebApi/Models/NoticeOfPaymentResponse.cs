using System.Text.Json.Serialization;

namespace CRS.WebApi.Models
{
    public class NoticeOfPaymentResponse
    {
        [JsonPropertyName("result")]
        public string Result { get; set; } = string.Empty;
    }
}
