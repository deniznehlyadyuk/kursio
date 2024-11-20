using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Courses;

public sealed class CourseStudent : Entity
{
    private CourseStudent()
    {
    }

    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public int PaymentAmount { get; private set; }

    public Student Student { get; private set; }
    public Course Course { get; private set; }

    public static CourseStudent Create(
        Guid studentId,
        Guid courseId,
        int paymentAmount)
    {
        return new CourseStudent
        {
            Id = Guid.NewGuid(),
            StudentId = studentId,
            CourseId = courseId,
            PaymentAmount = paymentAmount
        };
    }

    public void Update(int paymentAmount)
    {
        PaymentAmount = paymentAmount;
    }
}
