using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Classrooms;

namespace Kursio.Modules.Teachers.Application.Classrooms.DeleteClassroom;

internal sealed class DeleteClassroomCommandHandler(
    IClassroomRepository classroomRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteClassroomCommand>
{
    public async Task<Result> Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
    {
        Classroom? classroom = await classroomRepository.FindAsync(request.Id);

        if (classroom is null)
        {
            return Result.Failure(ClassroomErrors.NotFound(request.Id));
        }

        if (classroom.AssociatedCourseCount > 0)
        {
            return Result.Failure(ClassroomErrors.CannotDeleteClassroomWithCourses(request.Id));
        }

        classroomRepository.Remove(classroom);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
