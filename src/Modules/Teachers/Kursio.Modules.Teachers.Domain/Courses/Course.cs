using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Domain.Classrooms;
using Kursio.Modules.Teachers.Domain.Teachers;
using Kursio.Modules.Teachers.Domain.Topics;

namespace Kursio.Modules.Teachers.Domain.Courses;

public sealed class Course : Entity
{
    private Course()
    {
    }

    public Guid Id { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public Guid TeacherId { get; private set; }
    public Guid ClassroomId { get; private set; }
    public Guid TopicId { get; private set; }

    public Teacher Teacher { get; private set; }
    public Classroom Classroom { get; private set; }
    public Topic Topic { get; private set; }

    public static Course Create(
        DayOfWeek dayOfWeek,
        TimeSpan startTime,
        TimeSpan duration,
        Guid teacherId,
        Guid classroomId,
        Guid topicId)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            DayOfWeek = dayOfWeek,
            StartTime = startTime,
            Duration = duration,
            TeacherId = teacherId,
            ClassroomId = classroomId,
            TopicId = topicId
        };

        course.Raise(new CourseCreatedDomainEvent(course.Id, course.TopicId, course.ClassroomId));

        return course;
    }

    public void Update(
        DayOfWeek dayOfWeek,
        TimeSpan startTime,
        TimeSpan duration,
        Guid teacherId,
        Guid classroomId)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        Duration = duration;
        TeacherId = teacherId;
        ClassroomId = classroomId;

        Raise(new CourseUpdatedDomainEvent(Id, classroomId));
    }
}
