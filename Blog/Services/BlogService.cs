using AutoMapper;
using Blog.Api.Models;
using Blog.Api.Repositories;
using Blog.Api.Support.Inputs;
using Blog.Api.Support.Model;
using FluentValidation;

namespace Blog.Api.Services {
  public interface IBlogService {
    Task<List<BlogPostDto>> GetBlogPostAsync();
    Task<BlogPost?> GetBlogPostByIdAsync(int id);
    Task<ApplicationResponse> CreateBlogPostAsync(BlogPostInput blogPostInput);
    Task<ApplicationResponse> CreateCommentAsync(int id, CommentInput commentInput);
  }

  public class BlogService : IBlogService {
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<BlogPostInput> _blogPostInputvalidator;
    private readonly IValidator<CommentInput> _commentInputvalidator;
    private readonly IMapper _mapper;

    public BlogService(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository, IValidator<BlogPostInput> blogPostInputvalidator, IValidator<CommentInput> commentInputvalidator, IMapper mapper) {
      _blogPostRepository = blogPostRepository;
      _commentRepository = commentRepository;
      _blogPostInputvalidator = blogPostInputvalidator;
      _commentInputvalidator = commentInputvalidator;
      _mapper = mapper;
    }

    public async Task<List<BlogPostDto>> GetBlogPostAsync() {
      return await _blogPostRepository.GetBlogPostAsync();
    }

    public async Task<BlogPost?> GetBlogPostByIdAsync(int id) {
      return await _blogPostRepository.GetBlogPostByIdAsync(id);
    }

    public async Task<ApplicationResponse> CreateBlogPostAsync(BlogPostInput blogPostInput) {
      var response = new ApplicationResponse();
      try {
        if (blogPostInput == null) {
          response.Message = "Invalid input.";
          return response;
        }

        var validationResult = await _blogPostInputvalidator.ValidateAsync(blogPostInput);
        if (!validationResult.IsValid) {
          response.Message = $"Invalid input: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}";
          return response;
        }

        var blogPost = _mapper.Map<BlogPost>(blogPostInput);
        response.IsSuccess = await _blogPostRepository.CreateBlogPostAsync(blogPost);
        if (!response.IsSuccess) {
          response.Message = "An error has ocurred";
        }
        response.Message = "Blog Post created.";
      }
      catch (Exception ex) {
        response.Message = ex.InnerException?.Message != null ? $"{ex.Message} - InnerException: {ex.InnerException.Message}" : ex.Message;
      }
      return response;
    }

    public async Task<ApplicationResponse> CreateCommentAsync(int id, CommentInput commentInput) {
      var response = new ApplicationResponse();
      try {
        if (commentInput == null || id == 0) {
          response.Message = "Invalid input.";
          return response;
        }

        if (await _blogPostRepository.GetBlogPostByIdAsync(id) == null) {
          response.Message = "Post does not exist";
          return response;
        }

        var validationResult = await _commentInputvalidator.ValidateAsync(commentInput);
        if (!validationResult.IsValid) {
          response.Message = $"Invalid input: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}";
          return response;
        }

        var comment = _mapper.Map<Comment>(commentInput);
        comment.BlogPostId = id;
        response.IsSuccess = await _commentRepository.CreateCommentAsync(comment);
        if (!response.IsSuccess) {
          response.Message = "An error has ocurred";
        }
        response.Message = "Comment created.";
      }
      catch (Exception ex) {
        response.Message = ex.InnerException?.Message != null ? $"{ex.Message} - InnerException: {ex.InnerException.Message}" : ex.Message;
      }
      return response;
    }
  }
}