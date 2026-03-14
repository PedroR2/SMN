using Blog.Api.Context;
using Blog.Api.Mappers;
using Blog.Api.Repositories;
using Blog.Api.Services;
using Blog.Api.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class Program {
  public static void Main(string[] args) {
    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.UseUrls("https://localhost:5001", "http://localhost:5000");

    builder.Services.AddCors(options => {
      options.AddPolicy("AllowSpecificOrigin",
          policy => policy
              .WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod());
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddAutoMapper(typeof(BlogPostMapper));
    builder.Services.AddAutoMapper(typeof(CommentMapper));

    builder.Services.AddValidatorsFromAssemblyContaining<BlogPostInputValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<CommentInputValidator>();
    builder.Services.AddDbContext<BlogDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    builder.Services.AddScoped<IBlogService, BlogService>();
    builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseCors("AllowSpecificOrigin");

    app.MapControllers();

    app.Run();
  }
}