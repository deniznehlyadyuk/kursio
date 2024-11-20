using Kursio.Common.Application.QueryBuilding;
using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;

namespace Kursio.Common.Infrastructure.QueryBuilding;

internal sealed class QueryBuilder : IQueryBuilder
{
    private readonly List<IQueryStrategy> _strategies;

    public QueryBuilder()
    {
        _strategies = [];
    }

    public void AddStrategy(IQueryStrategy strategy)
    {
        _strategies.Add(strategy);
    }

    public Result<QueryBuilderResult> BuildQuery(string baseQuery)
    {
        List<string> queries = [baseQuery];
        Dictionary<string, object> parameters = [];

        foreach (IQueryStrategy strategy in _strategies.OrderBy(strategy => strategy.Priority))
        {
            Result<QueryBuilderResult> result = strategy.Apply();

            if (result.IsFailure)
            {
                return Result.Failure<QueryBuilderResult>(result.Error);
            }

            queries.Add(result.Value.Query);

            parameters = parameters.Union(result.Value.Parameters).ToDictionary();
        }

        return new QueryBuilderResult
        {
            Query = string.Join(" ", queries),
            Parameters = parameters
        };
    }
}
