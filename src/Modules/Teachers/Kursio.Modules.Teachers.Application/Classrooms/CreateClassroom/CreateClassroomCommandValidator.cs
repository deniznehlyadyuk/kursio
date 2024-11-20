using FluentValidation;

namespace Kursio.Modules.Teachers.Application.Classrooms.CreateClassroom;

internal sealed class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
{
    public CreateClassroomCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}
