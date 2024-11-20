using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Teachers.Application.Topics.GetTopic;

namespace Kursio.Modules.Teachers.Application.Topics.GetTopics;
public sealed record GetTopicsQuery(
    QueryBuilderFilterModel Filter,
    QueryBuilderSortingModel Sorting,
    QueryBuilderPaginationModel Pagination) : IQuery<IReadOnlyCollection<TopicResponse>>;
