using Kursio.Common.Application.Messaging;

namespace Kursio.Modules.Teachers.Application.Topics.GetTopic;
public sealed record GetTopicQuery(Guid Id) : IQuery<TopicResponse>;
