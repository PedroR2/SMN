using Blog.Api.Support.Model;

namespace Blog.Api.Models {
  public class BlogPost : BaseModel {
    public string Title { get; set; }
    public string Content { get; set; }
    public virtual List<Comment> Comments { get; set; }
  }
}