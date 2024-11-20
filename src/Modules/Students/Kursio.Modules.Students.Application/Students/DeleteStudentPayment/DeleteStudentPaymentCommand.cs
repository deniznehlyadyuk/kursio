using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.DeleteStudentPayment;
public sealed record DeleteStudentPaymentCommand(Guid Id) : ICommand<Guid>;
