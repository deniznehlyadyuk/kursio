using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Classrooms.GetClassroom;
public sealed record GetClassroomQuery(Guid Id) : IQuery<ClassroomResponse>;
