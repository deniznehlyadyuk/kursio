using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Classrooms;

namespace Kursio.Modules.Teachers.Application.Classrooms.CreateClassroom;

internal sealed class CreateClassroomCommandHandler(
    IClassroomRepository classroomRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateClassroomCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        Classroom? duplicateClassroom = await classroomRepository.FindByNameAsync(request.Name, cancellationToken);

        if (duplicateClassroom is not null)
        {
            return Result.Failure<Guid>(ClassroomErrors.DuplicateName(request.Name));
        }

        var classroom = Classroom.Create(request.Name);

        await classroomRepository.InsertAsync(classroom, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return classroom.Id;
    }
}
