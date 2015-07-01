namespace NativeCode.Sqlite.QueryBuilder.Extensions
{
    using System.Collections.Generic;

    public static class EntityColumnExtensions
    {
        public static string Join(this IEnumerable<string> columns)
        {
            return string.Join(", ", columns);
        }
    }
}