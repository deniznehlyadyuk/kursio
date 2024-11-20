using FluentValidation;

namespace Kursio.Modules.Teachers.Application.Teachers.UpdateTeacher;
internal sealed class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
{
    public UpdateTeacherCommandValidator()
    {
        RuleFor(c => c.FullName).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(10);
    }
}
