using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SemihBerkeKilic_FinalProject.Models;

namespace SemihBerkeKilic_FinalProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users { get; set; }
    }
}
