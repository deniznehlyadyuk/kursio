using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.DeleteStudents;

public sealed record DeleteStudentsCommand(IEnumerable<Guid> Ids) : ICommand;
