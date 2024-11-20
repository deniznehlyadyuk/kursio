using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Classrooms;
public sealed class Classroom : Entity
{
    private Classroom()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int AssociatedCourseCount { get; private set; }

    public static Classroom Create(string name)
    {
        return new Classroom
        {
            Id = Guid.NewGuid(),
            Name = name,
        };
    }

    public void Update(string name)
    {
        Name = name;

        Raise(new ClassroomUpdatedDomainEvent(Id, name));
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
