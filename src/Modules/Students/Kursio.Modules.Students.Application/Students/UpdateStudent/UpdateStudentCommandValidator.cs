using FluentValidation;

namespace Kursio.Modules.Students.Application.Students.UpdateStudent;

internal sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(c => c.FullName).NotEmpty();
    }
}
