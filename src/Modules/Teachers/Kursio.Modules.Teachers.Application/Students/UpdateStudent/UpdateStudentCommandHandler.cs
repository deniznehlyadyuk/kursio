using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Courses;

namespace Kursio.Modules.Teachers.Application.Students.UpdateStudent;
internal sealed class UpdateStudentCommandHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateStudentCommand>
{
    public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        Student? student = await studentRepository.FindAsync(request.Id);

        if (student is null)
        {
            return Result.Failure(StudentErrors.NotFound(request.Id));
        }

        student.Update(request.FullName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
