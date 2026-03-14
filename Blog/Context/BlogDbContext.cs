using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Context {
  public class BlogDbContext : DbContext {
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options) {
    }

    public DbSet<BlogPost> BlogPost { get; set; }
    public DbSet<Comment> Comment { get; set; }
  }
}