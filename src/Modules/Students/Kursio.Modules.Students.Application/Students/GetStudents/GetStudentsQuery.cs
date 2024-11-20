using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Students.Application.Students.GetStudent;

namespace Kursio.Modules.Students.Application.Students.GetStudents;

public sealed record GetStudentsQuery(
    QueryBuilderFilterModel Filter,
    QueryBuilderSortingModel Sorting,
    QueryBuilderPaginationModel Pagination) : IQuery<IReadOnlyCollection<StudentResponse>>;
