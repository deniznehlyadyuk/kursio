using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Teachers;

public sealed class TeacherUpdatedDomainEvent(
    Guid teacherId,
    string fullName) : DomainEvent
{
    public Guid TeacherId { get; private set; } = teacherId;
    public string FullName { get; private set; } = fullName;
}
