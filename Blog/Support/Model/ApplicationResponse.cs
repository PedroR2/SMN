namespace Blog.Api.Support.Model {
  public class ApplicationResponse {
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public ApplicationResponse() {
      IsSuccess = false;
      Message = "";
    }
  }
}
