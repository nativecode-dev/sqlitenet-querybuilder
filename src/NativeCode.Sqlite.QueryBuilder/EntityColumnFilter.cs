namespace NativeCode.Sqlite.QueryBuilder
{
    public class EntityColumnFilter
    {
        internal EntityColumnFilter(EntityColumn column, string groupName, FilterExpression expression, FilterComparison comparison)
        {
            this.Column = column;
            this.Comparison = comparison;
            this.Expression = expression;
            this.GroupName = groupName;
        }

        public EntityColumn Column { get; private set; }

        public FilterComparison Comparison { get; private set; }

        public FilterExpression Expression { get; private set; }

        public string GroupName { get; private set; }
    }
}