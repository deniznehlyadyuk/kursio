using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Teachers;

namespace Kursio.Modules.Teachers.Application.Teachers.CreateTeacher;

internal sealed class CreateTeacherCommandHandler(
    ITeacherRepository topicRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTeacherCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = Teacher.Create(request.FullName, request.PhoneNumber);

        await topicRepository.InsertAsync(teacher, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return teacher.Id;
    }
}
