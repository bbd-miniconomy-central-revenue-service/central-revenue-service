﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRS.WebApi.Models
{
    public class NoticeOfPaymentRequest
    {
        [Required]
        [JsonPropertyName("taxId")]
        public required Guid TaxId { get; set; }

        [Required]
        [JsonPropertyName("paymentId")]
        public required int PaymentId { get; set; }
    }
}
