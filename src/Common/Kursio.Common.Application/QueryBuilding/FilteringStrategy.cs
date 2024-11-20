using System.Text.Json;
using Kursio.Common.Application.Extensions;
using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;

namespace Kursio.Common.Application.QueryBuilding;

public sealed class FilteringStrategy(
    QueryBuilderFilterModel filterModel,
    Dictionary<string, string> columnMapping) : IQueryStrategy
{
    public QueryBuilderStrategyPriority Priority { get; init; } = QueryBuilderStrategyPriority.Filtering;


    public Result<QueryBuilderResult> Apply()
    {
        if (!filterModel.FilterGroups.Any())
        {
            return new QueryBuilderResult();
        }

        Result<string> queryResult = BuildFilterGroup(filterModel.FilterGroups, out Dictionary<string, object> parameters);

        if (queryResult.IsFailure)
        {
            return Result.Failure<QueryBuilderResult>(queryResult.Error);
        }

        return new QueryBuilderResult
        {
            Query = "WHERE " + queryResult.Value,
            Parameters = parameters
        };
    }

    private Result<string> BuildFilterGroup(IReadOnlyCollection<QueryBuilderFilterGroup> filterGroups, out Dictionary<string, object> parameters)
    {
        List<string> filters = [];
        parameters = [];

        foreach (QueryBuilderFilterGroup group in filterGroups)
        {
            List<string> groupFilters = [];

            foreach (QueryBuilderFilter filter in group.Filters)
            {
                string parameterName = $"param{parameters.Count}";

                Result<string> filterSqlResult = filter.FieldType.GetValueFromDescription<QueryBuilderFieldType>() switch
                {
                    QueryBuilderFieldType.Text => BuildTextFilter(filter, parameterName, parameters),
                    QueryBuilderFieldType.Number => BuildNumericFilter(filter, parameterName, parameters),
                    _ => throw new NotSupportedException($"Unsupported filter type: {filter.FieldType}"),
                };

                if (filterSqlResult.IsFailure)
                {
                    return Result.Failure<string>(filterSqlResult.Error);
                }

                groupFilters.Add(filterSqlResult.Value);
            }

            if (group.SubGroups.Any())
            {
                Result<string> subGroupQueryResult = BuildFilterGroup(group.SubGroups, out Dictionary<string, object> subgroupParameters);

                if (subGroupQueryResult.IsFailure)
                {
                    return subGroupQueryResult;
                }

                groupFilters.Add(subGroupQueryResult.Value);

                parameters = parameters.Union(subgroupParameters).ToDictionary();
            }

            filters.Add($"({string.Join($" {group.LogicalOperator} ", groupFilters)})");
        }

        return string.Join(" AND ", filters);
    }

    private Result<string> BuildTextFilter(QueryBuilderFilter filter, string parameterName, Dictionary<string, object> parameters)
    {
        bool mappingExists = columnMapping.TryGetValue(filter.Field, out string field);

        if (!mappingExists)
        {
            return Result.Failure<string>(QueryBuilderErrors.InvalidColumnNameUsage(filter.Field));
        }

        parameters[parameterName] = ((JsonElement)filter.Value).GetString()!;

        return filter.Operator.GetValueFromDescription<QueryBuilderTextOperator>() switch
        {
            QueryBuilderTextOperator.Equals => $"{field} = @{parameterName}",
            QueryBuilderTextOperator.NotEquals => $"{field} != @{parameterName}",
            QueryBuilderTextOperator.Contains => $"{field} LIKE CONCAT('%', @{parameterName}, '%')",
            QueryBuilderTextOperator.StartsWith => $"{field} LIKE CONCAT(@{parameterName}, '%')",
            QueryBuilderTextOperator.EndsWith => $"{field} LIKE CONCAT('%', @{parameterName})",
            _ => throw new NotSupportedException($"Unsupported text operator: {filter.Operator}")
        };
    }

    private Result<string> BuildNumericFilter(QueryBuilderFilter filter, string parameterName, Dictionary<string, object> parameters)
    {
        bool mappingExists = columnMapping.TryGetValue(filter.Field, out string field);

        if (!mappingExists)
        {
            return Result.Failure<string>(QueryBuilderErrors.InvalidColumnNameUsage(filter.Field));
        }

        parameters[parameterName] = ((JsonElement)filter.Value).GetDouble()!;

        switch (filter.Operator.GetValueFromDescription<QueryBuilderNumberOperator>())
        {
            case QueryBuilderNumberOperator.Equals:
                return $"{field} = @{parameterName}";
            case QueryBuilderNumberOperator.NotEquals:
                return $"{field} != @{parameterName}";
            case QueryBuilderNumberOperator.LessThan:
                return $"{field} < @{parameterName}";
            case QueryBuilderNumberOperator.LessThanOrEqual:
                return $"{field} <= @{parameterName}";
            case QueryBuilderNumberOperator.GreaterThan:
                return $"{field} > @{parameterName}";
            case QueryBuilderNumberOperator.GreaterThanOrEqual:
                return $"{field} >= @{parameterName}";
            case QueryBuilderNumberOperator.Between:
                parameters[parameterName + "to"] = ((JsonElement)filter.ValueTo).GetDouble()!;
                return $"{field} BETWEEN @{parameterName} AND @{parameterName + "to"}";
            default:
                throw new NotSupportedException($"Unsupported numeric operator: {filter.Operator}");
        }
    }
}
