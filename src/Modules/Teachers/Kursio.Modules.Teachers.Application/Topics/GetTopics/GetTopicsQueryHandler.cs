using System.Data.Common;
using Dapper;
using Kursio.Common.Application.Data;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Application.QueryBuilding;
using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Teachers.Application.Topics.GetTopic;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Kursio.Modules.Teachers.Application.Topics.GetTopics;
internal sealed class GetTopicQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IQueryBuilder queryBuilder,
    ILogger<GetTopicQueryHandler> logger) : IQueryHandler<GetTopicsQuery, IReadOnlyCollection<TopicResponse>>
{
    public async Task<Result<IReadOnlyCollection<TopicResponse>>> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string baseQuery =
            $"""
            SELECT
                id AS {nameof(TopicResponse.Id)},
                name AS {nameof(TopicResponse.Name)}
            FROM teachers.topics
            """;

        queryBuilder.AddStrategy(new FilteringStrategy(
            request.Filter,
            new Dictionary<string, string>()
            {
                { "name", "teachers.topics.name" }
            }));

        queryBuilder.AddStrategy(new SortingStrategy(
            request.Sorting,
            new Dictionary<string, string>()
            {
                { "name", "teachers.topics.name" }
            }));

        queryBuilder.AddStrategy(new PaginationStrategy(request.Pagination));

        Result<QueryBuilderResult> buildResult = queryBuilder.BuildQuery(baseQuery);

        if (buildResult.IsFailure)
        {
            return Result.Failure<IReadOnlyCollection<TopicResponse>>(buildResult.Error);
        }

        using (LogContext.PushProperty("SqlQuery", buildResult.Value.Query))
        {
            logger.LogInformation("Topics fetching");
        }

        List<TopicResponse> topics =
            (await connection.QueryAsync<TopicResponse>(buildResult.Value.Query, buildResult.Value.Parameters))
                .AsList();

        return topics;
    }
}
