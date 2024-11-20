using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Topics.DeleteTopic;
public sealed record DeleteTopicCommand(Guid Id) : ICommand;
