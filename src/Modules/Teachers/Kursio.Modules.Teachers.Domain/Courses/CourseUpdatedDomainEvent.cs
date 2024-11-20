using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Courses;

public sealed class CourseUpdatedDomainEvent(
    Guid courseId,
    Guid classroomId) : DomainEvent
{
    public Guid CourseId { get; private set; } = courseId;
    public Guid ClassroomId { get; private set; } = classroomId;
}
