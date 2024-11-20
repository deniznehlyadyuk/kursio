using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Classrooms;

namespace Kursio.Modules.Teachers.Application.Classrooms.UpdateClassroom;
internal sealed class UpdateClassroomCommandHandler(
    IClassroomRepository classroomRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateClassroomCommand>
{
    public async Task<Result> Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
    {
        Classroom? duplicateClassroom = await classroomRepository.FindByNameAsync(request.Name, cancellationToken);

        if (duplicateClassroom is not null && duplicateClassroom.Id != request.Id)
        {
            return Result.Failure<Guid>(ClassroomErrors.DuplicateName(request.Name));
        }

        Classroom? classroom = await classroomRepository.FindAsync(request.Id);

        if (classroom is null)
        {
            return Result.Failure(ClassroomErrors.NotFound(request.Id));
        }

        classroom.Update(request.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
