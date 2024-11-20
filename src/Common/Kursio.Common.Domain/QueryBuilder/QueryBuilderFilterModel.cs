namespace Kursio.Common.Domain.QueryBuilder;

public class QueryBuilderFilterModel
{
    public IReadOnlyCollection<QueryBuilderFilterGroup> FilterGroups { get; set; } = [];
}
