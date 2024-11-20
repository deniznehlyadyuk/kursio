using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;

namespace Kursio.Common.Application.QueryBuilding;

public interface IQueryStrategy
{
    QueryBuilderStrategyPriority Priority { get; init; }
    Result<QueryBuilderResult> Apply();
}
