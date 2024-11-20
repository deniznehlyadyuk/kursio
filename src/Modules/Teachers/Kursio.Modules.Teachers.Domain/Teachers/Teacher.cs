using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Teachers;

public sealed class Teacher : Entity
{
    private Teacher()
    {
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string PhoneNumber { get; private set; }
    public int AssociatedCourseCount { get; private set; }

    public static Teacher Create(string fullName, string phoneNumber)
    {
        var teacher = new Teacher()
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            PhoneNumber = phoneNumber
        };

        teacher.Raise(new TeacherCreatedDomainEvent(teacher.Id, teacher.FullName));

        return teacher;
    }

    public void Update(string fullName, string phoneNumber)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;

        Raise(new TeacherUpdatedDomainEvent(Id, fullName));
    }

    public void Remove()
    {
        Raise(new TeacherRemovedDomainEvent(Id));
    }

    public void IncreaseAssociatedCourseCount()
    {
        AssociatedCourseCount++;
    }

    public void DecreaseAssociatedCourseCount()
    {
        AssociatedCourseCount--;
    }
}
