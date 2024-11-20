using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.UpdateStudentPayment;

public sealed record UpdateStudentPaymentCommand(Guid Id, int PaymentAmount) : ICommand<Guid>;
