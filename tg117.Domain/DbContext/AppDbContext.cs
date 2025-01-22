using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tg117.Domain.Models;

namespace tg117.Domain.DbContext
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<CourseContent> CourseContent { get; set; }

        public DbSet<CourseContentDetails> CourseContentDetails { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Frequency> Frequency { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<OrderHeader> OrderHeader { get; set; }
    }
}