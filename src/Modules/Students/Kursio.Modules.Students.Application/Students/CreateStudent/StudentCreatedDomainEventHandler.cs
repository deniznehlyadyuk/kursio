using Kursio.Common.Application.EventBus;
using Kursio.Common.Application.Exceptions;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Students.Application.Students.GetStudent;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.IntegrationEvents;
using MediatR;

namespace Kursio.Modules.Students.Application.Students.CreateStudent;

internal sealed class StudentCreatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : IDomainEventHandler<StudentCreatedDomainEvent>
{
    public async Task Handle(StudentCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<StudentResponse> result = await sender.Send(new GetStudentQuery(notification.StudentId), cancellationToken);

        if (result.IsFailure)
        {
            throw new KursioException(nameof(GetStudentQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new StudentCreatedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                result.Value.Id,
                result.Value.FullName),
            cancellationToken);
    }
}
