using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRS.WebApi;

public class TaxRecord
{
    public Guid TaxId { get; set; }
    public int TaxPayerType { get; set; }
    public int HasPaid { get; set; }

    [Column("AmountOwing", TypeName = "money")]
    public decimal AmountOwing { get; set; }
    public string? TaxType { get; set; }

    [Column("PaymentAmount", TypeName = "money")]
    public decimal PaymentAmount { get; set; }
}
