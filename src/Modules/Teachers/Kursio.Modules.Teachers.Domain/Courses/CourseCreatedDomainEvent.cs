using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Courses;
public sealed class CourseCreatedDomainEvent(
    Guid courseId,
    Guid topicId,
    Guid classroomId) : DomainEvent
{
    public Guid CourseId { get; private set; } = courseId;
    public Guid TopicId { get; private set; } = topicId;
    public Guid ClassroomId { get; private set; } = classroomId;
}
