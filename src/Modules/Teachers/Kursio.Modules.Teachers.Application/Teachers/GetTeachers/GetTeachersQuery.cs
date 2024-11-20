using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Teachers.Application.Teachers.GetTeacher;

namespace Kursio.Modules.Teachers.Application.Teachers.GetTeachers;
public sealed record GetTeachersQuery(
    QueryBuilderFilterModel Filter,
    QueryBuilderSortingModel Sorting,
    QueryBuilderPaginationModel Pagination) : IQuery<IReadOnlyCollection<TeacherResponse>>;
