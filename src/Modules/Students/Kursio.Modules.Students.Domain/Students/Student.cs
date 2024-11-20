using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Domain.Students;

public sealed class Student : Entity
{
    private Student()
    {
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? ParentFullName { get; private set; }
    public string? ParentPhoneNumber { get; private set; }
    public int Debt { get; private set; }

    public IList<StudentPayment> Payments { get; private set; }

    public static Result<Student> Create(string fullName, string? phoneNumber, string? parentFullName, string? parentPhoneNumber)
    {
        var student = new Student
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            PhoneNumber = phoneNumber,
            ParentFullName = parentFullName,
            ParentPhoneNumber = parentPhoneNumber,
            Debt = 0
        };

        student.Raise(new StudentCreatedDomainEvent(student.Id));

        return student;
    }

    public void DecreaseDebt(int paymentAmount)
    {
        Debt -= paymentAmount;
    }

    public void Update(string fullName, string phoneNumber, string parentFullName, string parentPhoneNumber)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        ParentFullName = parentFullName;
        ParentPhoneNumber = parentPhoneNumber;

        Raise(new StudentUpdatedDomainEvent(Id));
    }

    public void Remove()
    {
        Raise(new StudentDeletedDomainEvent(Id));
    }

    public void UpdateDebt(int paymentAmountDiff)
    {
        Debt -= paymentAmountDiff;
    }
}
