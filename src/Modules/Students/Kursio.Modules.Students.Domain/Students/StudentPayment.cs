using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Domain.Students;

public sealed class StudentPayment : Entity
{
    private StudentPayment()
    {
    }

    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public int PaymentAmount { get; private set; }
    public DateTime DateTimeUtc { get; set; }

    public Student Student { get; private set; }

    public static Result<StudentPayment> Create(Guid studentId, int paymentAmount)
    {
        if (paymentAmount < 0)
        {
            return Result.Failure<StudentPayment>(StudentErrors.InvalidPaymentAmount);
        }

        var studentPayment = new StudentPayment
        {
            Id = Guid.NewGuid(),
            StudentId = studentId,
            PaymentAmount = paymentAmount,
            DateTimeUtc = DateTime.UtcNow
        };

        studentPayment.Raise(new StudentPaymentCreatedDomainEvent(studentId, paymentAmount));

        return studentPayment;
    }

    public void Remove()
    {
        Raise(new StudentPaymentDeletedDomainEvent(StudentId, PaymentAmount));
    }

    public Result Update(int paymentAmount)
    {
        if (paymentAmount < 0)
        {
            return Result.Failure<StudentPayment>(StudentErrors.InvalidPaymentAmount);
        }

        Raise(new StudentPaymentUpdatedDomainEvent(StudentId, paymentAmount - PaymentAmount));

        PaymentAmount = paymentAmount;

        return Result.Success();
    }
}
