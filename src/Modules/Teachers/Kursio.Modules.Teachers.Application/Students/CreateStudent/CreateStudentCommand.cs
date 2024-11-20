using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Students.CreateStudent;

public sealed record CreateStudentCommand(Guid Id, string FullName) : ICommand;
