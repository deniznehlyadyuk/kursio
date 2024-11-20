using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Teachers;
public sealed class TeacherRemovedDomainEvent(Guid teacherId) : DomainEvent
{
    public Guid TeacherId { get; private set; } = teacherId;
}
