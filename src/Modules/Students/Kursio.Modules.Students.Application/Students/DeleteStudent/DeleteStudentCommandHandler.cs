using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Application.Students.DeleteStudent;

internal sealed class DeleteStudentCommandHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteStudentCommand>
{
    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        Student? student = await studentRepository.FindAsync(request.Id);

        if (student is null)
        {
            return Result.Failure(StudentErrors.NotFound(request.Id));
        }

        student.Remove();

        studentRepository.Remove(student);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
