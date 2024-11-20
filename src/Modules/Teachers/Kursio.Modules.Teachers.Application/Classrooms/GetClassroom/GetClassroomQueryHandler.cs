using System.Data.Common;
using Dapper;
using Kursio.Common.Application.Data;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Domain.Classrooms;

namespace Kursio.Modules.Teachers.Application.Classrooms.GetClassroom;
internal sealed class GetClassroomQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetClassroomQuery, ClassroomResponse>
{
    public async Task<Result<ClassroomResponse>> Handle(GetClassroomQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
            SELECT
                id AS {nameof(ClassroomResponse.Id)},
                name AS {nameof(ClassroomResponse.Name)}
            FROM teachers.classrooms
            WHERE id = @Id
            """;

        ClassroomResponse? classroom = await connection.QuerySingleOrDefaultAsync<ClassroomResponse>(sql, request);

        if (classroom is null)
        {
            return Result.Failure<ClassroomResponse>(ClassroomErrors.NotFound(request.Id));
        }

        return new ClassroomResponse(request.Id, classroom.Name);
    }
}
