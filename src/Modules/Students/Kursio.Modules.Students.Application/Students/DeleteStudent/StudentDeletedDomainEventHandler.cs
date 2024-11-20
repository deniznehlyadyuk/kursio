using Kursio.Common.Application.EventBus;
using Kursio.Common.Application.Messaging;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.IntegrationEvents;

namespace Kursio.Modules.Students.Application.Students.DeleteStudent;

internal sealed class StudentDeletedDomainEventHandler(
    IEventBus eventBus) : IDomainEventHandler<StudentDeletedDomainEvent>
{
    public async Task Handle(StudentDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new StudentDeletedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.StudentId),
            cancellationToken);
    }
}
