using Blog.Api.Support.Inputs;
using FluentValidation;

namespace Blog.Api.Validators {
  // Dev Note: Não foi passada nenhuma business rule no projeto, aqui eu assumi algumas básicas apenas pra exemplificar uma implementação de validação simples.
  public class CommentInputValidator : AbstractValidator<CommentInput> {
    public CommentInputValidator() {
      RuleFor(c => c.Content)
      .NotEmpty().WithMessage("Content is required.")
      .MaximumLength(100).WithMessage("Content cannot be longer than 100 characters.")
      .Must(x => x is string).WithMessage("Content must be a valid string.");
    }
  }
}