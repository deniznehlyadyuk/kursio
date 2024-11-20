using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Teachers.GetTeacher;
public sealed record GetTeacherQuery(Guid Id) : IQuery<TeacherResponse>;
