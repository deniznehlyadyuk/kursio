using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Topics.DeleteTopics;
public sealed record DeleteTopicsCommand(IEnumerable<Guid> Ids) : ICommand;
