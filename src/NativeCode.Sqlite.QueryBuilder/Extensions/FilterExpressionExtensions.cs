namespace NativeCode.Sqlite.QueryBuilder.Extensions
{
    public static class FilterExpressionExtensions
    {
        public static string Stringify(this FilterExpression expression)
        {
            switch (expression)
            {
                case FilterExpression.Or:
                    return "OR";

                default:
                    return "AND";
            }
        }
    }
}