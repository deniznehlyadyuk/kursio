using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Application.Students.UpdateStudent;

internal sealed class UpdateStudentCommandHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateStudentCommand>
{
    public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        Student? student = await studentRepository.FindAsync(request.Id);

        if (student is null)
        {
            return Result.Failure<Guid>(StudentErrors.NotFound(request.Id));
        }

        student.Update(request.FullName, request.PhoneNumber, request.ParentFullName, request.ParentPhoneNumber);

        studentRepository.Update(student);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
