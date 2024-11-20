using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Domain.Students;

public sealed class StudentCreatedDomainEvent(Guid studentId) : DomainEvent
{
    public Guid StudentId { get; init; } = studentId;
}
