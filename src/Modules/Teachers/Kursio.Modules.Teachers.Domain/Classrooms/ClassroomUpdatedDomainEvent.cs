using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Classrooms;

public sealed class ClassroomUpdatedDomainEvent(
    Guid classroomId,
    string name) : DomainEvent
{
    public Guid ClassroomId { get; private set; } = classroomId;
    public string Name { get; private set; } = name;
}
