using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Teachers;

namespace Kursio.Modules.Teachers.Application.Teachers.DeleteTeachers;

internal sealed class DeleteTeachersCommandHandler(
    ITeacherRepository teacherRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteTeachersCommand>
{
    public async Task<Result> Handle(DeleteTeachersCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.RemoveAllAsync(request.Ids, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
