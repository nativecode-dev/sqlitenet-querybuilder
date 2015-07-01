namespace NativeCode.Sqlite.QueryBuilder.TemplateBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class QueryTemplateBuilder
    {
        protected readonly StringBuilder Template = new StringBuilder();

        private readonly List<EntityColumn> filters = new List<EntityColumn>();

        protected QueryTemplateBuilder(EntityTable table)
        {
            this.Table = table;
        }

        protected QueryTemplateBuilder(params EntityTable[] tables)
        {
            this.Tables = tables;
        }

        public IReadOnlyList<EntityColumn> Filters
        {
            get { return this.filters; }
        }

        protected EntityTable Table { get; private set; }

        protected IEnumerable<EntityTable> Tables { get; private set; }

        public void AddFilter(EntityColumn column)
        {
            this.filters.Add(column);
        }

        public QueryTemplate BuildTemplate()
        {
            return QueryTemplate.Parse(this.ToString());
        }

        public void Reset()
        {
            this.Template.Clear();
        }

        public override string ToString()
        {
            this.Template.Clear();
            this.BuildQuery();

            return this.Template.ToString();
        }

        protected abstract void BuildQuery();

        protected string GetColumnName(EntityColumn column)
        {
            var name = column.Name;

            if (QueryBuilder.Configuration.QualifyColumnNames)
            {
                name = this.QualifyString(name);
            }

            if (QueryBuilder.Configuration.UseColumnAlias)
            {
                name += " AS " + this.QualifyString(column.Name);
            }

            return name;
        }

        protected string GetColumnValue(EntityColumn column)
        {
            var value = column.GetAsToken();

            if (column.IsQuotable)
            {
                value = this.QuotedString(value);
            }

            return value;
        }

        protected string GetTableName(EntityTable table)
        {
            var name = table.Name;

            if (QueryBuilder.Configuration.QualifyTableNames)
            {
                name = this.QualifyString(name);
            }

            if (QueryBuilder.Configuration.UseTableAlias)
            {
                name += " AS " + this.QualifyString(table.Alias);
            }

            return name;
        }

        protected string GetWhereByPrimaryKeys(EntityTable table)
        {
            var columns = from primaryKey in table.GetCompositeKeys()
                          let key = this.GetColumnName(primaryKey)
                          let value = this.GetColumnValue(primaryKey)
                          select key + " = " + value;

            return "WHERE " + string.Join(" AND ", columns);
        }

        protected string QualifyString(string value)
        {
            return "[" + value + "]";
        }

        protected string QuotedString(string value)
        {
            if (QueryBuilder.Configuration.UseDoubleQuotes)
            {
                return "\"" + value + "\"";
            }

            return "'" + value + "'";
        }
    }
}