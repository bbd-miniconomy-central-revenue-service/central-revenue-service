using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

[Table("TaxPayment")]
public partial class TaxPayment
{
    [Key]
    [Column("id", TypeName = "bigint")]
    public long Id { get; set; }

    [Column("taxPayerID", TypeName = "bigint")]
    public long TaxPayerId { get; set; }

    [Column("amount", TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("taxType")]
    public int TaxType { get; set; }

    [Column("created")]
    public DateTime? Created { get; set; }

    [ForeignKey("TaxPayerId")]
    [InverseProperty("TaxPayments")]
    public virtual TaxPayer TaxPayer { get; set; } = null!;

    [ForeignKey("TaxType")]
    [InverseProperty("TaxPayments")]
    public virtual TaxType TaxTypeNavigation { get; set; } = null!;
}
