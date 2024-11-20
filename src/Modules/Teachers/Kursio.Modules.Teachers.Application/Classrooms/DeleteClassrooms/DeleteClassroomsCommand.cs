using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Classrooms.DeleteClassrooms;
public sealed record DeleteClassroomsCommand(IEnumerable<Guid> Ids) : ICommand;
