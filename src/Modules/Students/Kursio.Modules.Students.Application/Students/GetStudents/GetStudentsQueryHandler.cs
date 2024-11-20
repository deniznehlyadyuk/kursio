using System.Data.Common;
using Dapper;
using Kursio.Common.Application.Data;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Application.QueryBuilding;
using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Students.Application.Students.GetStudent;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Kursio.Modules.Students.Application.Students.GetStudents;

internal sealed class GetStudentsQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IQueryBuilder queryBuilder,
    ILogger<GetStudentsQueryHandler> logger)
    : IQueryHandler<GetStudentsQuery, IReadOnlyCollection<StudentResponse>>
{
    public async Task<Result<IReadOnlyCollection<StudentResponse>>> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string baseQuery =
            $"""
            SELECT
                id AS {nameof(StudentResponse.Id)},
                full_name AS {nameof(StudentResponse.FullName)},
                phone_number AS {nameof(StudentResponse.PhoneNumber)},
                parent_full_name AS {nameof(StudentResponse.ParentFullName)},
                parent_phone_number AS {nameof(StudentResponse.ParentPhoneNumber)},
                debt AS {nameof(StudentResponse.Debt)}
            FROM students.students
            """;

        queryBuilder.AddStrategy(new FilteringStrategy(
            request.Filter,
            new Dictionary<string, string>()
            {
                { "fullName", "students.students.full_name" },
                { "phoneNumber", "students.students.phone_number" },
                { "parentFullName", "students.students.parent_full_name" },
                { "parentPhoneNumber", "students.students.parent_phone_number" },
                { "debt", "students.students.debt" },
            }));

        queryBuilder.AddStrategy(new SortingStrategy(
            request.Sorting,
            new Dictionary<string, string>()
            {
                { "fullName", "students.students.full_name" },
                { "debt", "students.students.debt" },
            }));

        queryBuilder.AddStrategy(new PaginationStrategy(request.Pagination));

        Result<QueryBuilderResult> buildResult = queryBuilder.BuildQuery(baseQuery);

        if (buildResult.IsFailure)
        {
            return Result.Failure<IReadOnlyCollection<StudentResponse>>(buildResult.Error);
        }

        using (LogContext.PushProperty("SqlQuery", buildResult.Value.Query))
        {
            logger.LogInformation("Students fetching");
        }

        List<StudentResponse> students =
            (await connection.QueryAsync<StudentResponse>(buildResult.Value.Query, buildResult.Value.Parameters))
                .AsList();

        return students;
    }
}
