using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Domain.Students;

public sealed class StudentUpdatedDomainEvent(Guid studentId) : DomainEvent
{
    public Guid StudentId { get; init; } = studentId;
}
