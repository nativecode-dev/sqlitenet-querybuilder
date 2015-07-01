namespace NativeCode.Sqlite.QueryBuilder
{
    public class QueryBuilder
    {
        private static QueryBuilderConfiguration currentConfiguration = QueryBuilderConfiguration.Default;

        public static QueryBuilderConfiguration Current
        {
            get { return currentConfiguration; }
            set { currentConfiguration = value; }
        }
    }
}