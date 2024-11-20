using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Students.UpdateStudent;
public sealed record UpdateStudentCommand(Guid Id, string FullName) : ICommand;
