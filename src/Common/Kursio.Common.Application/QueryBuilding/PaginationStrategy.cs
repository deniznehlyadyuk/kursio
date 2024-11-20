using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;

namespace Kursio.Common.Application.QueryBuilding;

public sealed class PaginationStrategy(QueryBuilderPaginationModel paginationModel) : IQueryStrategy
{
    public QueryBuilderStrategyPriority Priority { get; init; } = QueryBuilderStrategyPriority.Pagination;

    public Result<QueryBuilderResult> Apply()
    {
        int offset = (paginationModel.Page - 1) * paginationModel.PageSize;

        Dictionary<string, object> parameters = new()
        {
            { "PageSize", paginationModel.PageSize },
            { "Offset", offset },
        };

        return new QueryBuilderResult
        {
            Query = $"LIMIT @PageSize OFFSET @Offset",
            Parameters = parameters
        };
    }
}
