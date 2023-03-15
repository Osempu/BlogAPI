using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>().HasData(
                new Post {Id = 1, Author = "Oscar Montenegro", Title = "My first Post", Body = "Hello world, this is my first post"},
                new Post {Id = 2, Author = "Oscar Montenegro", Title = "My second Post", Body = "Hello world, this is my second post"}
            );
        }

        public DbSet<Post> Posts { get; set; }
    }
}