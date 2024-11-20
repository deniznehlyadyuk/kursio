using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Teachers.DeleteTeachers;
public sealed record DeleteTeachersCommand(IEnumerable<Guid> Ids) : ICommand;
