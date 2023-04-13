using BlogAPI.Models;
using FluentValidation;

namespace BlogAPI.Validators
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(x => x.Title)
                    .NotEmpty()
                    .MaximumLength(100)
                    .WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.Body)
                    .NotEmpty()
                    .MaximumLength(500)
                    .WithMessage("The body of the post cannot exceed 500 characters");

            RuleFor(x => x.Author)
                    .NotEmpty()
                    .MaximumLength(100)
                    .WithMessage("The name of the author cannot exceed 100 characters");
        }
    }
}