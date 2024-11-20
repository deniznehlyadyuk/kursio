namespace Kursio.Common.Domain.QueryBuilder;

public sealed class QueryBuilderResult
{
    public string Query { get; init; } = "";
    public Dictionary<string, object> Parameters { get; init; } = [];
}
