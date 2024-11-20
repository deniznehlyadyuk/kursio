using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.DeleteStudent;

public sealed record DeleteStudentCommand(Guid Id) : ICommand;
