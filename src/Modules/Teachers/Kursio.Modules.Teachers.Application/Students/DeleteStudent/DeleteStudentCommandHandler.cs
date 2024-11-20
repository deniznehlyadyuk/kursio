using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Courses;

namespace Kursio.Modules.Teachers.Application.Students.DeleteStudent;
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

        studentRepository.Remove(student);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
