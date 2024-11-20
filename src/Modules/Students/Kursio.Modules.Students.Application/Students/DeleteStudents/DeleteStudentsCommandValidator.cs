using FluentValidation;

namespace Kursio.Modules.Students.Application.Students.DeleteStudents;
internal sealed class DeleteStudentsCommandValidator : AbstractValidator<DeleteStudentsCommand>
{
    public DeleteStudentsCommandValidator()
    {
        RuleFor(r => r.Ids).NotEmpty();
    }
}
