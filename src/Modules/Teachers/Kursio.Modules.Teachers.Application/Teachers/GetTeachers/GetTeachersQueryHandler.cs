using System.Data.Common;
using Dapper;
using Kursio.Common.Application.Data;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Application.QueryBuilding;
using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Teachers.Application.Teachers.GetTeacher;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Kursio.Modules.Teachers.Application.Teachers.GetTeachers;
internal sealed class GetTeachersQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IQueryBuilder queryBuilder,
    ILogger<GetTeachersQueryHandler> logger) : IQueryHandler<GetTeachersQuery, IReadOnlyCollection<TeacherResponse>>
{
    public async Task<Result<IReadOnlyCollection<TeacherResponse>>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string baseQuery =
            $"""
            SELECT
                id AS {nameof(TeacherResponse.Id)},
                full_name AS {nameof(TeacherResponse.FullName)},
                phone_number AS {nameof(TeacherResponse.PhoneNumber)}
            FROM teachers.teachers
            """;

        queryBuilder.AddStrategy(new FilteringStrategy(
            request.Filter,
            new Dictionary<string, string>()
            {
                { "fullName", "teachers.teachers.full_name" },
                { "phoneNumber", "teachers.teachers.phone_number" }
            }));

        queryBuilder.AddStrategy(new SortingStrategy(
            request.Sorting,
            new Dictionary<string, string>()
            {
                { "fullName", "teachers.teachers.full_name" }
            }));

        queryBuilder.AddStrategy(new PaginationStrategy(request.Pagination));

        Result<QueryBuilderResult> buildResult = queryBuilder.BuildQuery(baseQuery);

        if (buildResult.IsFailure)
        {
            return Result.Failure<IReadOnlyCollection<TeacherResponse>>(buildResult.Error);
        }

        using (LogContext.PushProperty("SqlQuery", buildResult.Value.Query))
        {
            logger.LogInformation("Teachers fetching");
        }

        List<TeacherResponse> teachers =
            (await connection.QueryAsync<TeacherResponse>(buildResult.Value.Query, buildResult.Value.Parameters))
                .AsList();

        return teachers;
    }
}
