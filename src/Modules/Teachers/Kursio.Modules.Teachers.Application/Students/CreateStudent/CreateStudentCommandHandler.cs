using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Courses;

namespace Kursio.Modules.Teachers.Application.Students.CreateStudent;

internal sealed class CreateStudentCommandHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateStudentCommand>
{
    public async Task<Result> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = Student.Create(request.Id, request.FullName);

        await studentRepository.InsertAsync(student, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
