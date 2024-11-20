using System.Data.Common;
using Dapper;
using Kursio.Common.Application.Data;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Domain.Topics;

namespace Kursio.Modules.Teachers.Application.Topics.GetTopic;
internal sealed class GetTopicsQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetTopicQuery, TopicResponse>
{
    public async Task<Result<TopicResponse>> Handle(GetTopicQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
            SELECT
                id AS {nameof(TopicResponse.Id)},
                name AS {nameof(TopicResponse.Name)}
            FROM teachers.topics
            WHERE id = @Id
            """;

        TopicResponse? topic = await connection.QuerySingleOrDefaultAsync<TopicResponse>(sql, request);

        if (topic is null)
        {
            return Result.Failure<TopicResponse>(TopicErrors.NotFound(request.Id));
        }

        return new TopicResponse(request.Id, topic.Name);
    }
}
