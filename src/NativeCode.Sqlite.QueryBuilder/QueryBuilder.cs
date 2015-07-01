namespace NativeCode.Sqlite.QueryBuilder
{
    public class QueryBuilder
    {
        private static QueryBuilderConfiguration current = QueryBuilderConfiguration.Default;

        public static QueryBuilderConfiguration Configuration
        {
            get { return current; }
            set { current = value; }
        }
    }
}