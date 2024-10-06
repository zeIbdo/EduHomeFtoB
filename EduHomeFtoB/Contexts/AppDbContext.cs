using EduHomeFtoB.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHomeFtoB.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
