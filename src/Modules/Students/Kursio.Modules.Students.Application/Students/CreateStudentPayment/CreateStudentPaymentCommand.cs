using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.CreateStudentPayment;

public sealed record CreateStudentPaymentCommand(
    Guid StudentId,
    int PaymentAmount) : ICommand<Guid>;
