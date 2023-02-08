using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoJA2.Models;

namespace ToDoJA2.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoJA2.Models.Accessories> Accessories { get; set; } = default!;
        public DbSet<ToDoJA2.Models.ToDoItems> ToDoItems { get; set; } = default!;
    }
}