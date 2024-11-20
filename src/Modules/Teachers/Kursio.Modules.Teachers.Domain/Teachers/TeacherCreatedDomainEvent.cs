using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Teachers;

public sealed class TeacherCreatedDomainEvent(
    Guid teacherId,
    string fullName) : DomainEvent
{
    public Guid TeacherId { get; init; } = teacherId;
    public string FullName { get; init; } = fullName;
}
