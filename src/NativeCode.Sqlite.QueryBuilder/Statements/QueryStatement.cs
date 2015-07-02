namespace NativeCode.Sqlite.QueryBuilder.Statements
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NativeCode.Sqlite.QueryBuilder.Attributes;

    public abstract class QueryStatement
    {
        protected static readonly string Space = " ";

        protected readonly List<EntityColumnFilter> Filters = new List<EntityColumnFilter>();

        protected readonly List<EntityColumn> Selections = new List<EntityColumn>();

        protected readonly List<EntityColumnSort> Sortables = new List<EntityColumnSort>();

        protected internal QueryStatement(string keyword, EntityTable table)
        {
            this.Keyword = keyword;
            this.Table = table;
        }

        public string Keyword { get; private set; }

        protected EntityTable Table { get; private set; }

        public virtual bool CanBeginStatement(QueryStatement current)
        {
            return current == null;
        }

        protected internal void Filter(
            EntityColumn column,
            string @group = "Default",
            FilterCondition condition = FilterCondition.Default,
            FilterComparison comparison = FilterComparison.Default)
        {
            this.Filters.Add(new EntityColumnFilter(column, @group, condition, comparison));
        }

        protected internal void Filter(
            IEnumerable<EntityColumn> columns,
            string @group = "Default",
            FilterCondition condition = FilterCondition.Default,
            FilterComparison comparison = FilterComparison.Default)
        {
            this.Filters.AddRange(columns.Select(c => new EntityColumnFilter(c, @group, condition, comparison)));
        }

        protected internal void Select(EntityColumn column)
        {
            this.Selections.Add(column);
        }

        protected internal void Select(IEnumerable<EntityColumn> columns)
        {
            this.Selections.AddRange(columns);
        }

        protected internal void Sort(EntityColumn column, SortDirection direction = SortDirection.Default)
        {
            this.Sortables.Add(new EntityColumnSort(column, direction));
        }

        protected internal void Sort(IEnumerable<EntityColumn> columns)
        {
            this.Sortables.AddRange(columns.Select(c => new EntityColumnSort(c, SortDirection.Default)));
        }

        protected internal abstract void WriteTo(StringBuilder template);
    }
}