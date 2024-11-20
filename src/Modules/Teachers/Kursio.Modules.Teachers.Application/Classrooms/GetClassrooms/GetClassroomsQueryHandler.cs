using System.Data.Common;
using Dapper;
using Kursio.Common.Application.Data;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Application.QueryBuilding;
using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Teachers.Application.Classrooms.GetClassroom;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Kursio.Modules.Teachers.Application.Classrooms.GetClassrooms;
internal sealed class GetClassroomsQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IQueryBuilder queryBuilder,
    ILogger<GetClassroomsQueryHandler> logger) : IQueryHandler<GetClassroomsQuery, IReadOnlyCollection<ClassroomResponse>>
{
    public async Task<Result<IReadOnlyCollection<ClassroomResponse>>> Handle(GetClassroomsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string baseQuery =
            $"""
            SELECT
                id AS {nameof(ClassroomResponse.Id)},
                name AS {nameof(ClassroomResponse.Name)}
            FROM teachers.classrooms
            """;

        queryBuilder.AddStrategy(new FilteringStrategy(
            request.Filter,
            new Dictionary<string, string>()
            {
                { "name", "teachers.classrooms.name" }
            }));

        queryBuilder.AddStrategy(new SortingStrategy(
            request.Sorting,
            new Dictionary<string, string>()
            {
                { "name", "teachers.classrooms.name" }
            }));

        queryBuilder.AddStrategy(new PaginationStrategy(request.Pagination));

        Result<QueryBuilderResult> buildResult = queryBuilder.BuildQuery(baseQuery);

        if (buildResult.IsFailure)
        {
            return Result.Failure<IReadOnlyCollection<ClassroomResponse>>(buildResult.Error);
        }

        using (LogContext.PushProperty("SqlQuery", buildResult.Value.Query))
        {
            logger.LogInformation("Classrooms fetching");
        }

        List<ClassroomResponse> classrooms =
            (await connection.QueryAsync<ClassroomResponse>(buildResult.Value.Query, buildResult.Value.Parameters))
                .AsList();

        return classrooms;
    }
}
