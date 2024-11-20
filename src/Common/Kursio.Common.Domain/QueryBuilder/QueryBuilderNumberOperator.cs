using System.ComponentModel;

namespace Kursio.Common.Domain.QueryBuilder;

public enum QueryBuilderNumberOperator
{
    [Description("Equals")]
    Equals,
    [Description("NotEquals")]
    NotEquals,
    [Description("Between")]
    Between,
    [Description("LessThan")]
    LessThan,
    [Description("LessThanOrEqual")]
    LessThanOrEqual,
    [Description("GreaterThan")]
    GreaterThan,
    [Description("GreaterThanOrEqual")]
    GreaterThanOrEqual
}
