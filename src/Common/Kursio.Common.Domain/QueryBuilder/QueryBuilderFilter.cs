namespace Kursio.Common.Domain.QueryBuilder;

public class QueryBuilderFilter
{
    public string Field { get; set; }
    public string FieldType { get; set; }
    public object Value { get; set; }
    public object ValueTo { get; set; }
    public string Operator { get; set; }
}
