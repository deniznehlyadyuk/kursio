using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Domain.Students;

public sealed class StudentPaymentUpdatedDomainEvent(
    Guid studentId,
    int paymentAmountDiff) : DomainEvent
{
    public Guid StudentId { get; init; } = studentId;
    public int PaymentAmountDiff { get; init; } = paymentAmountDiff;
}
