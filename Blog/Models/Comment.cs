using System.ComponentModel.DataAnnotations.Schema;
using Blog.Api.Support.Model;

namespace Blog.Api.Models {
  public class Comment : BaseModel {
    public string Content { get; set; }
    [Column("BlogPost_ID")]
    public int BlogPostId { get; set; }
  }
}
