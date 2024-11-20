using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Students.Application.Students.GetStudent;

public sealed record GetStudentQuery(Guid StudentId) : IQuery<StudentResponse>;
