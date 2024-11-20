using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Classrooms.DeleteClassroom;
public sealed record DeleteClassroomCommand(Guid Id) : ICommand;
