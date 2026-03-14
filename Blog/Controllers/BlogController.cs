using Blog.Api.Models;
using Blog.Api.Services;
using Blog.Api.Support.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers {
  [Route("api/posts")]
  [ApiController]
  public class BlogController : ControllerBase {
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService) {
      _blogService = blogService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPostDto>>> Get() {
      try {
        var blogPosts = await _blogService.GetBlogPostAsync();
        if (blogPosts == null || blogPosts.Count == 0) {
          return NoContent();
        }

        return Ok(blogPosts);
      }
      catch (Exception ex) {
        throw new Exception(
            ex.InnerException?.Message != null ? $"{ex.Message} - InnerException: {ex.InnerException.Message}" : ex.Message,
            ex
        );
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPost>> GetBlogPostById(int id) {
      try {
        var blogPost = await _blogService.GetBlogPostByIdAsync(id);
        if (blogPost == null) {
          return NotFound($"Post with ID {id} not found.");
        }

        return Ok(blogPost);
      }
      catch (Exception ex) {
        throw new Exception(
            ex.InnerException?.Message != null ? $"{ex.Message} - InnerException: {ex.InnerException.Message}" : ex.Message,
            ex
        );
      }
    }

    [HttpPost]
    public async Task<ActionResult> CreateBlogPost([FromBody] BlogPostInput blogPostInput) {
      try {
        var response = await _blogService.CreateBlogPostAsync(blogPostInput);

        if (!response.IsSuccess) {
          return BadRequest(response);
        }

        return Ok(response);
      }
      catch (Exception ex) {
        throw new Exception(
            ex.InnerException?.Message != null ? $"{ex.Message} - InnerException: {ex.InnerException.Message}" : ex.Message,
            ex
        );
      }
    }

    [HttpPost("{id}/comments")]
    public async Task<ActionResult> CreateComment(int id, [FromBody] CommentInput commentInput) {
      try {
        var response = await _blogService.CreateCommentAsync(id, commentInput);

        if (!response.IsSuccess) {
          return BadRequest(response);
        }

        return Ok(response);
      }
      catch (Exception ex) {
        throw new Exception(
            ex.InnerException?.Message != null ? $"{ex.Message} - InnerException: {ex.InnerException.Message}" : ex.Message,
            ex
        );
      }
    }
  }
}