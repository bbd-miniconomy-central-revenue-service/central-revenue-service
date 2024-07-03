using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRS.WebApi.Repositories;

public class UserRepository(CrsdbContext context) : GenericRepository<User, int>(context)
{
    public async Task<User?> GetByEmail(string email)
    {
        return (await Find(user => user.Email == email)).FirstOrDefault();
    }
}