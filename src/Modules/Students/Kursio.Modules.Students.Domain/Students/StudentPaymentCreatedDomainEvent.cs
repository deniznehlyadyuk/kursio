using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Domain.Students;

public sealed class StudentPaymentCreatedDomainEvent(Guid studentId, int paymentAmount) : DomainEvent
{
    public Guid StudentId { get; init; } = studentId;
    public int PaymentAmount { get; init; } = paymentAmount;
}
