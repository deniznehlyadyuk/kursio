namespace Kursio.Common.Domain.QueryBuilder;

public class QueryBuilderSortingModel
{
    public IReadOnlyCollection<QueryBuilderSortField> Fields { get; set; } = [];
}
