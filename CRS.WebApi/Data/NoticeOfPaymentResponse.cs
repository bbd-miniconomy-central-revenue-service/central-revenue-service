using System.Text.Json.Serialization;

namespace CRS.WebApi.Data
{
    public class NoticeOfPaymentResponse
    {
        [JsonPropertyName("result")]
        public string Result { get; set; } = string.Empty;
    }
}
