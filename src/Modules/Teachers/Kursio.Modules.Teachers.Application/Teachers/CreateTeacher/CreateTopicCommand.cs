using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Teachers.CreateTeacher;
public sealed record CreateTeacherCommand(string FullName, string PhoneNumber) : ICommand<Guid>;
