using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Classrooms.UpdateClassroom;

public sealed record UpdateClassroomCommand(Guid Id, string Name) : ICommand;
