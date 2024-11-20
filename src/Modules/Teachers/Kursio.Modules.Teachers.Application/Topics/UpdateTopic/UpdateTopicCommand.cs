using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Topics.UpdateTopic;

public sealed record UpdateTopicCommand(Guid Id, string Name) : ICommand;
