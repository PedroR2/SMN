using Blog.Api.Context;
using Blog.Api.Models;

namespace Blog.Api.Repositories {
  public interface ICommentRepository {
    Task<bool> CreateCommentAsync(Comment comment);
  }

  public class CommentRepository : ICommentRepository {
    private readonly BlogDbContext _context;

    public CommentRepository(BlogDbContext context) {
      _context = context;
    }

    public async Task<bool> CreateCommentAsync(Comment comment) {
      try {
        await _context.Comment.AddAsync(comment);
        return await _context.SaveChangesAsync() > 0;
      }
      catch (Exception ex) {
        throw;
      }
    }
  }
}