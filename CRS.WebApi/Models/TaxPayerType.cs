using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

[Table("TaxPayerType")]
public partial class TaxPayerType
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

    [InverseProperty("GroupNavigation")]
    public virtual ICollection<TaxPayer> TaxPayers { get; set; } = [];
}
