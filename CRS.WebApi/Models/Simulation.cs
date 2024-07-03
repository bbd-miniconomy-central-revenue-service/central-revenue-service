using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

[Table("Simulation")]
public partial class Simulation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("startTime")]
    public DateTime StartTime { get; set; }

    [InverseProperty("SimulationNavigation")]
    public virtual ICollection<TaxPayer> TaxPayers { get; set; } = [];
}
