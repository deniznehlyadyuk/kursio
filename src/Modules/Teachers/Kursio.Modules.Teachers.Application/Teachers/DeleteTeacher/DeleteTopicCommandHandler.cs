using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Teachers;

namespace Kursio.Modules.Teachers.Application.Teachers.DeleteTeacher;

internal sealed class DeleteTeacherCommandHandler(
    ITeacherRepository teacherRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteTeacherCommand>
{
    public async Task<Result> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        Teacher? teacher = await teacherRepository.FindAsync(request.Id);

        if (teacher is null)
        {
            return Result.Failure(TeacherErrors.NotFound(request.Id));
        }

        if (teacher.AssociatedCourseCount > 0)
        {
            return Result.Failure(TeacherErrors.CannotDeleteTeacherWithCourses(request.Id));
        }

        teacherRepository.Remove(teacher);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
