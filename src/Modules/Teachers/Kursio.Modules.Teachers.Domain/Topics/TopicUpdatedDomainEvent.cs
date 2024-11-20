using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Topics;
public sealed class TopicUpdatedDomainEvent(
    Guid topicId,
    string name) : DomainEvent
{
    public Guid TopicId { get; private set; } = topicId;
    public string Name { get; private set; } = name;
}
