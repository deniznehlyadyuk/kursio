using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Classrooms.CreateClassroom;
public sealed record CreateClassroomCommand(string Name) : ICommand<Guid>;
