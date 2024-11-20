using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Application.Students.CreateStudent;

internal sealed class CreateStudentCommandHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateStudentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        Result<Student> result = Student.Create(
            request.FullName,
            request.PhoneNumber,
            request.ParentFullName,
            request.ParentPhoneNumber
        );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await studentRepository.InsertAsync(result.Value, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
