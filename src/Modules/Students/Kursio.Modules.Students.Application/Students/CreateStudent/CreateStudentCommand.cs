using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.CreateStudent;

public sealed record CreateStudentCommand(
    string FullName,
    string PhoneNumber,
    string ParentFullName,
    string ParentPhoneNumber) : ICommand<Guid>;
