namespace server.Models.Dtos;

public class UserCreateDto
{
    public string Username { get; set; }

    public string UserPicUrl { get; set; }

    public string Email { get; set; }

    public string UserPicUrl { get; set; }

    public int RoleId { get; set; }

    public User ToUser()
    {
        return new User
        {
            Email = Email,
            Username = Username,
            UserPicUrl = UserPicUrl
        };
    }
}
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("created")]
    public DateTime? Created { get; set; }