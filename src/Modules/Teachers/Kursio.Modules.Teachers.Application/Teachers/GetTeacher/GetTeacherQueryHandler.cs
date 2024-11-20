using System.Data.Common;
using Dapper;
using Kursio.Common.Application.Data;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Domain.Teachers;

namespace Kursio.Modules.Teachers.Application.Teachers.GetTeacher;
internal sealed class GetTeacherQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetTeacherQuery, TeacherResponse>
{
    public async Task<Result<TeacherResponse>> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
            SELECT
                id AS {nameof(TeacherResponse.Id)},
                full_name AS {nameof(TeacherResponse.FullName)},
                phone_number AS {nameof(TeacherResponse.PhoneNumber)}
            FROM teachers.teachers
            WHERE id = @Id
            """;

        TeacherResponse? teacher = await connection.QuerySingleOrDefaultAsync<TeacherResponse>(sql, request);

        if (teacher is null)
        {
            return Result.Failure<TeacherResponse>(TeacherErrors.NotFound(request.Id));
        }

        return new TeacherResponse(teacher.Id, teacher.FullName, teacher.PhoneNumber);
    }
}
