using FluentValidation;

namespace Kursio.Modules.Teachers.Application.Teachers.CreateTeacher;

internal sealed class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherCommandValidator()
    {
        RuleFor(c => c.FullName).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(10);
    }
}
