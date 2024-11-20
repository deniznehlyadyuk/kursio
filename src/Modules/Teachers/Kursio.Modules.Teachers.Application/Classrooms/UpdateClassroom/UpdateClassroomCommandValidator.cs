using FluentValidation;

namespace Kursio.Modules.Teachers.Application.Classrooms.UpdateClassroom;
internal sealed class UpdateClassroomCommandValidator : AbstractValidator<UpdateClassroomCommand>
{
    public UpdateClassroomCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}
