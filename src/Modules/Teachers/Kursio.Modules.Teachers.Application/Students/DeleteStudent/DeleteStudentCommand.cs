using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Students.DeleteStudent;
public sealed record DeleteStudentCommand(Guid Id) : ICommand;
