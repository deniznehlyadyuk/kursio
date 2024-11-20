using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Teachers;

namespace Kursio.Modules.Teachers.Application.Teachers.UpdateTeacher;
internal sealed class UpdateTeacherCommandHandler(
    ITeacherRepository teacherRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateTeacherCommand>
{
    public async Task<Result> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        Teacher? teacher = await teacherRepository.FindAsync(request.Id);

        if (teacher is null)
        {
            return Result.Failure(TeacherErrors.NotFound(request.Id));
        }

        teacher.Update(request.FullName, request.PhoneNumber);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
