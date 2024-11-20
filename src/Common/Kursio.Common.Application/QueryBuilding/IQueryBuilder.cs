using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;

namespace Kursio.Common.Application.QueryBuilding;

public interface IQueryBuilder
{
    void AddStrategy(IQueryStrategy strategy);

    Result<QueryBuilderResult> BuildQuery(string baseQuery);
}
