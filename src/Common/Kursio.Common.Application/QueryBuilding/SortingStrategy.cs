using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;

namespace Kursio.Common.Application.QueryBuilding;

public sealed class SortingStrategy(
    QueryBuilderSortingModel sortingModel,
    Dictionary<string, string> columnMapping) : IQueryStrategy
{
    public QueryBuilderStrategyPriority Priority { get; init; } = QueryBuilderStrategyPriority.Sorting;

    public Result<QueryBuilderResult> Apply()
    {
        if (!sortingModel.Fields.Any())
        {
            return new QueryBuilderResult();
        }

        List<string> orderByClauses = [];

        foreach (QueryBuilderSortField field in sortingModel.Fields)
        {
            bool mappingExists = columnMapping.TryGetValue(field.Field, out string columnName);

            if (!mappingExists)
            {
                return Result.Failure<QueryBuilderResult>(QueryBuilderErrors.InvalidColumnNameUsage(field.Field));
            }

            orderByClauses.Add($"{columnName} {(field.Desc ? "DESC" : "")}");
        }

        return new QueryBuilderResult
        {
            Query = $"ORDER BY {string.Join(", ", orderByClauses)}",
            Parameters = []
        };
    }
}
