using System.Data.Common;
using Dapper;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Application.Data;
using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Application.Students.GetStudent;

internal sealed class GetStudentsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetStudentQuery, StudentResponse>
{
    public async Task<Result<StudentResponse>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
            SELECT
                id AS {nameof(StudentResponse.Id)},
                full_name AS {nameof(StudentResponse.FullName)},
                phone_number AS {nameof(StudentResponse.PhoneNumber)},
                parent_full_name AS {nameof(StudentResponse.ParentFullName)},
                parent_phone_number AS {nameof(StudentResponse.ParentPhoneNumber)},
                debt AS {nameof(StudentResponse.Debt)}
            FROM students.students
            WHERE id = @StudentId
            """;

        StudentResponse? student = await connection.QuerySingleOrDefaultAsync<StudentResponse>(sql, request);

        if (student is null)
        {
            return Result.Failure<StudentResponse>(StudentErrors.NotFound(request.StudentId));
        }

        return student;
    }
}
