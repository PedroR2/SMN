using Blog.Api.Context;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Repositories {
  public interface IBlogPostRepository {
    Task<List<BlogPostDto>> GetBlogPostAsync();
    Task<BlogPost?> GetBlogPostByIdAsync(int id);
    Task<bool> CreateBlogPostAsync(BlogPost blogPost);
  }

  public class BlogPostRepository : IBlogPostRepository {
    private readonly BlogDbContext _context;

    public BlogPostRepository(BlogDbContext context) {
      _context = context;
    }

    public async Task<List<BlogPostDto>> GetBlogPostAsync() {
      try {
        return await _context.BlogPost
            .Select(x => new BlogPostDto {
              Id = x.Id,
              Title = x.Title,
              Content = x.Content,
              CommentCount = x.Comments.Count()
            })
            .ToListAsync();
      }
      catch (Exception ex) {
        throw;
      }
    }

    public async Task<BlogPost?> GetBlogPostByIdAsync(int id) {
      try {
        return await _context.BlogPost.Where(x => x.Id == id)
          .Include(x => x.Comments)
          .SingleOrDefaultAsync();
      }
      catch (Exception ex) {
        throw;
      }
    }

    public async Task<bool> CreateBlogPostAsync(BlogPost blogPost) {
      try {
        await _context.BlogPost.AddAsync(blogPost);
        return await _context.SaveChangesAsync() > 0;
      }
      catch (Exception ex) {
        throw;
      }
    }
  }
}