namespace Kursio.Modules.Teachers.Domain.Topics;
public static class TopicCacheKeys
{
    public static string Topic(Guid id) => $"topic-{id}";
}
