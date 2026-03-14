using AutoMapper;
using Blog.Api.Models;
using Blog.Api.Support.Inputs;

namespace Blog.Api.Mappers {
  public class CommentMapper : Profile {
    public CommentMapper() {
      CreateMap<CommentInput, Comment>()
        .ForMember(c => c.Id, opt => opt.Ignore());
    }
  }
}