using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain.QueryBuilder;

namespace Kursio.Modules.Students.Application.Students.GetStudentPayments;
public sealed record GetStudentPaymentsQuery(
    Guid StudentId,
    QueryBuilderFilterModel Filter,
    QueryBuilderSortingModel Sorting,
    QueryBuilderPaginationModel Pagination) : ICommand<IReadOnlyCollection<StudentPaymentResponse>>;
