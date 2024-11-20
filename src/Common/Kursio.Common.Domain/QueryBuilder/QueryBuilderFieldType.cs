using System.ComponentModel;

namespace Kursio.Common.Domain.QueryBuilder;

public enum QueryBuilderFieldType
{
    [Description("String")]
    Text,
    [Description("Number")]
    Number
}
