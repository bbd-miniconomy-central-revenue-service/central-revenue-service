using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

[Table("TaxType")]
public partial class TaxType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("created")]
    public DateTime? Created { get; set; }

    [Column("rate", TypeName = "decimal(5, 5)")]
    public decimal Rate { get; set; }

    [InverseProperty("TaxTypeNavigation")]
    public virtual ICollection<TaxPayment> TaxPayments { get; set; } = new List<TaxPayment>();
}
