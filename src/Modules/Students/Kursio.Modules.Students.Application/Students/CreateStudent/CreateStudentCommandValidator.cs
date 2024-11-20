using FluentValidation;

namespace Kursio.Modules.Students.Application.Students.CreateStudent;

internal sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(c => c.FullName).NotEmpty();
    }
}
