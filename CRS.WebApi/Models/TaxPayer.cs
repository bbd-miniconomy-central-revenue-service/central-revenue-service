using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

[Table("TaxPayer")]
public partial class TaxPayer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("personaId")]
    public int? PersonaId { get; set; }

    [Column("taxPayerID")]
    public Guid TaxPayerId { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("group")]
    public int Group { get; set; }

    [Column("status")]
    public int Status { get; set; }

    [Column("created")]
    public DateTime? Created { get; set; }

    [Column("updated")]
    public DateTime? Updated { get; set; }

    [Column("amountOwing", TypeName = "money")]
    public decimal AmountOwing { get; set; }

    [ForeignKey("Group")]
    [InverseProperty("TaxPayers")]
    public virtual TaxPayerType GroupNavigation { get; set; } = null!;

    [ForeignKey("Status")]
    [InverseProperty("TaxPayers")]
    public virtual TaxStatus StatusNavigation { get; set; } = null!;

    [InverseProperty("TaxPayer")]
    public virtual ICollection<TaxPayment> TaxPayments { get; set; } = new List<TaxPayment>();
}
