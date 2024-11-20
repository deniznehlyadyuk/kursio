using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Teachers.DeleteTeacher;
public sealed record DeleteTeacherCommand(Guid Id) : ICommand;
