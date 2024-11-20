using Kursio.Common.Application.EventBus;

namespace Kursio.Modules.Students.IntegrationEvents;

public sealed class StudentDeletedIntegrationEvent : IntegrationEvent
{
    public Guid StudentId { get; init; }

    public StudentDeletedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid studentId) : base(id, occurredOnUtc)
    {
        StudentId = studentId;
    }
}
