using Kursio.Common.Application.Exceptions;
using Kursio.Common.Domain;
using Kursio.Modules.Students.IntegrationEvents;
using Kursio.Modules.Teachers.Application.Students.UpdateStudent;
using MassTransit;
using MediatR;

namespace Kursio.Modules.Teachers.Presentation.Students;

public sealed class StudentUpdatedIntegrationEventConsumer(
    ISender sender) : IConsumer<StudentUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<StudentUpdatedIntegrationEvent> context)
    {
        Result result = await sender.Send(new UpdateStudentCommand(context.Message.StudentId, context.Message.FullName));

        if (result.IsFailure)
        {
            throw new KursioException(nameof(UpdateStudentCommand), result.Error);
        }
    }
}
