using FluentValidation;
using LibrarySystem.Application.Books.Commands;

namespace LibrarySystem.Application.Validators
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ISBN).NotEmpty().Length(10, 13);
            RuleFor(x => x.AvailableCopies).GreaterThanOrEqualTo(1);
        }
    }
}
