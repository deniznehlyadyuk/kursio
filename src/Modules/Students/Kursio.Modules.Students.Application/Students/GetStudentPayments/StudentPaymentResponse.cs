namespace Kursio.Modules.Students.Application.Students.GetStudentPayments;

public sealed record StudentPaymentResponse(
    Guid Id,
    int PaymentAmount,
    DateTime DateTimeUtc);
