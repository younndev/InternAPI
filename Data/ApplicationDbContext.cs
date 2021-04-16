using InternAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InternAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Student> Student { get; set; }

    }
}