using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Teachers.UpdateTeacher;

public sealed record UpdateTeacherCommand(Guid Id, string FullName, string PhoneNumber) : ICommand;
