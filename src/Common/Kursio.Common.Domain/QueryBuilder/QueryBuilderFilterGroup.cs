namespace Kursio.Common.Domain.QueryBuilder;

public class QueryBuilderFilterGroup
{
    public IReadOnlyCollection<QueryBuilderFilter> Filters { get; set; } = [];
    public string LogicalOperator { get; set; }
    public IReadOnlyCollection<QueryBuilderFilterGroup> SubGroups { get; set; } = [];
}
