using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Classrooms;

namespace Kursio.Modules.Teachers.Application.Classrooms.DeleteClassrooms;

internal sealed class DeleteClassroomsCommandHandler(
    IClassroomRepository classroomRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteClassroomsCommand>
{
    public async Task<Result> Handle(DeleteClassroomsCommand request, CancellationToken cancellationToken)
    {
        await classroomRepository.RemoveAllAsync(request.Ids, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
