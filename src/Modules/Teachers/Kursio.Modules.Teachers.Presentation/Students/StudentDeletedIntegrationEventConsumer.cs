using Kursio.Common.Application.Exceptions;
using Kursio.Common.Domain;
using Kursio.Modules.Students.IntegrationEvents;
using Kursio.Modules.Teachers.Application.Students.DeleteStudent;
using MassTransit;
using MediatR;

namespace Kursio.Modules.Teachers.Presentation.Students;

public sealed class StudentDeletedIntegrationEventConsumer(
    ISender sender) : IConsumer<StudentDeletedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<StudentDeletedIntegrationEvent> context)
    {
        Result result = await sender.Send(new DeleteStudentCommand(context.Message.StudentId));

        if (result.IsFailure)
        {
            throw new KursioException(nameof(DeleteStudentCommand), result.Error);
        }
    }
}
