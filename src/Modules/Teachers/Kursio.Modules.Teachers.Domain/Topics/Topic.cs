using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Topics;

public sealed class Topic : Entity
{
    private Topic()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int AssociatedCourseCount { get; private set; }

    public static Topic Create(string name)
    {
        return new Topic
        {
            Id = Guid.NewGuid(),
            Name = name,
            AssociatedCourseCount = 0
        };
    }

    public void Update(string name)
    {
        Name = name;

        Raise(new TopicUpdatedDomainEvent(Id, name));
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
