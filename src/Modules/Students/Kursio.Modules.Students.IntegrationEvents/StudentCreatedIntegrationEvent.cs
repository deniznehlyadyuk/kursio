using Kursio.Common.Application.EventBus;

namespace Kursio.Modules.Students.IntegrationEvents;

public sealed class StudentCreatedIntegrationEvent : IntegrationEvent
{
    public Guid StudentId { get; init; }
    public string FullName { get; init; }

    public StudentCreatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid studentId,
        string fullName) : base(id, occurredOnUtc)
    {
        StudentId = studentId;
        FullName = fullName;
    }
}
