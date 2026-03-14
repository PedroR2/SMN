using AutoMapper;
using Blog.Api.Models;
using Blog.Api.Support.Inputs;

namespace Blog.Api.Mappers {
  public class BlogPostMapper : Profile {
    public BlogPostMapper() {
      CreateMap<BlogPostInput, BlogPost>()
        .ForMember(c => c.Id, opt => opt.Ignore())
        .ForMember(c => c.Comments, opt => opt.Ignore());
    }
  }
}