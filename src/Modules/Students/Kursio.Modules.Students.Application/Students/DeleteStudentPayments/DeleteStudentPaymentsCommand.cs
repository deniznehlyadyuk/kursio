using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.DeleteStudentPayments;

public sealed record DeleteStudentPaymentsCommand(
    IEnumerable<Guid> Ids) : ICommand<IEnumerable<Guid>>;
