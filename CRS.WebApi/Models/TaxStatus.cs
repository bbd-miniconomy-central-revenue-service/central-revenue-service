using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

[Table("TaxStatus")]
public partial class TaxStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<TaxPayer> TaxPayers { get; set; } = new List<TaxPayer>();
}
