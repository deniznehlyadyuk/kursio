using Kursio.Common.Application.Data;
using System.Data.Common;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Application.QueryBuilding;
using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Students.Application.Students.GetStudent;
using Serilog.Context;
using Microsoft.Extensions.Logging;
using Dapper;

namespace Kursio.Modules.Students.Application.Students.GetStudentPayments;

internal sealed class GetStudentPaymentsQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IQueryBuilder queryBuilder,
    ILogger<GetStudentsQueryHandler> logger)
    : ICommandHandler<GetStudentPaymentsQuery, IReadOnlyCollection<StudentPaymentResponse>>
{
    public async Task<Result<IReadOnlyCollection<StudentPaymentResponse>>> Handle(
        GetStudentPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string baseQuery =
            $"""
            SELECT
                students.student_payments.id AS {nameof(StudentPaymentResponse.Id)},
                students.student_payments.payment_amount AS {nameof(StudentPaymentResponse.PaymentAmount)},
                students.student_payments.date_time_utc AS {nameof(StudentPaymentResponse.DateTimeUtc)}
            FROM students.student_payments
            LEFT JOIN
                students.students ON students.students.id = '{request.StudentId}'
            """;

        Dictionary<string, string> columnMapping = new()
        {
            { "paymentAmount", "students.student_payments.payment_amount" },
            { "dateTimeUtc", "students.student_payments.date_time_utc" },
        };

        queryBuilder.AddStrategy(new FilteringStrategy(request.Filter, columnMapping));

        queryBuilder.AddStrategy(new SortingStrategy(request.Sorting, columnMapping));

        queryBuilder.AddStrategy(new PaginationStrategy(request.Pagination));

        Result<QueryBuilderResult> buildResult = queryBuilder.BuildQuery(baseQuery);

        if (buildResult.IsFailure)
        {
            return Result.Failure<IReadOnlyCollection<StudentPaymentResponse>>(buildResult.Error);
        }

        using (LogContext.PushProperty("SqlQuery", buildResult.Value.Query))
        {
            logger.LogInformation("Student payments fetching");
        }

        List<StudentPaymentResponse> studentPayments =
            (await connection.QueryAsync<StudentPaymentResponse>(buildResult.Value.Query, buildResult.Value.Parameters))
                .AsList();

        return studentPayments;
    }
}
