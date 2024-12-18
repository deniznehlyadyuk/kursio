﻿
namespace Kursio.Common.Domain.QueryBuilder;

public static class QueryBuilderErrors
{
    public static Error InvalidColumnNameUsage(string columnName)
    {
        return Error.Failure("QueryBuilder.InvalidColumnNameUsage", $"The column '{columnName}' used in your request is not permitted.");
    }
}
