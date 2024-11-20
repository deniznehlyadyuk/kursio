using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.DeleteStudents;
public sealed class DeleteStudentsCommandHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteStudentsCommand>
{
    public async Task<Result> Handle(
        DeleteStudentsCommand request,
        CancellationToken cancellationToken)
    {
        await studentRepository.RemoveAllAsync(request.Ids, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
