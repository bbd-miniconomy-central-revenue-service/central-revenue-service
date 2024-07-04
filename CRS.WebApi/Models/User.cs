using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRS.WebApi.Models;

[Table("User")]
[Index("Email", Name = "UQ__User__AB6E6164031D2F51", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("created")]
    public DateTime? Created { get; set; }

    public string? Username { get; set; } = null;
    public string? UserPicUrl { get; set; } = null;
}
