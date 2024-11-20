using Kursio.Common.Application.Exceptions;
using Kursio.Common.Domain;
using Kursio.Modules.Students.IntegrationEvents;
using Kursio.Modules.Teachers.Application.Students.CreateStudent;
using MassTransit;
using MediatR;

namespace Kursio.Modules.Teachers.Presentation.Students;
public sealed class StudentCreatedIntegrationEventConsumer(
    ISender sender) : IConsumer<StudentCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<StudentCreatedIntegrationEvent> context)
    {
        Result result = await sender.Send(new CreateStudentCommand(context.Message.StudentId, context.Message.FullName));

        if (result.IsFailure)
        {
            throw new KursioException(nameof(CreateStudentCommand), result.Error);
        }
    }
}
