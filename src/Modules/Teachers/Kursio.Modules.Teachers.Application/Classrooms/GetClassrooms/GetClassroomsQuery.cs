using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Modules.Teachers.Application.Classrooms.GetClassroom;

namespace Kursio.Modules.Teachers.Application.Classrooms.GetClassrooms;
public sealed record GetClassroomsQuery(
    QueryBuilderFilterModel Filter,
    QueryBuilderSortingModel Sorting,
    QueryBuilderPaginationModel Pagination) : IQuery<IReadOnlyCollection<ClassroomResponse>>;
