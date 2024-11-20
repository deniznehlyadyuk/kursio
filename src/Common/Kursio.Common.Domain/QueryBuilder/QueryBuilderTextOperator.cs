using System.ComponentModel;

namespace Kursio.Common.Domain.QueryBuilder;

public enum QueryBuilderTextOperator
{
    [Description("Equals")]
    Equals,
    [Description("NotEquals")]
    NotEquals,
    [Description("Contains")]
    Contains,
    [Description("StartsWith")]
    StartsWith,
    [Description("EndsWith")]
    EndsWith
}
