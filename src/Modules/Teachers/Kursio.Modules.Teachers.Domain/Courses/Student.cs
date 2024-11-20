using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Courses;

public sealed class Student : Entity
{
    private Student()
    {
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; }

    public IList<CourseStudent> Courses { get; private set; }

    public static Student Create(Guid id, string fullName)
    {
        return new Student
        {
            Id = id,
            FullName = fullName
        };
    }

    public void Update(string fullName)
    {
        FullName = fullName;
    }
}
