using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.UpdateStudent;

public sealed record UpdateStudentCommand(
    Guid Id,
    string FullName,
    string PhoneNumber,
    string ParentFullName,
    string ParentPhoneNumber) : ICommand;
