using Kursio.Common.Application.EventBus;

namespace Kursio.Modules.Students.IntegrationEvents;

public sealed class StudentUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid StudentId { get; init; }
    public string FullName { get; init; }

    public StudentUpdatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid studentId,
        string fullName) : base(id, occurredOnUtc)
    {
        StudentId = studentId;
        FullName = fullName;
    }
}
