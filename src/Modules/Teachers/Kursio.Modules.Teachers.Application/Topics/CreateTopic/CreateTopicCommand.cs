using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Topics.CreateTopic;
public sealed record CreateTopicCommand(string Name) : ICommand<Guid>;
