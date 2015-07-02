namespace NativeCode.Sqlite.QueryBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NativeCode.Sqlite.QueryBuilder.Attributes;
    using NativeCode.Sqlite.QueryBuilder.Exceptions;
    using NativeCode.Sqlite.QueryBuilder.Statements;

    public class QueryBuilder
    {
        private readonly StringBuilder template = new StringBuilder(200);

        private readonly Queue<QueryStatement> statements = new Queue<QueryStatement>();

        private QueryBuilder(EntityTable table)
        {
            this.Table = table;
        }

        protected QueryStatement CurrentStatement { get; private set; }

        protected EntityTable Table { get; private set; }

        public static QueryBuilder New(EntityTable table)
        {
            return new QueryBuilder(table);
        }

        public QueryBuilder And(EntityColumn column, FilterComparison comparison = FilterComparison.Equals)
        {
            this.CurrentStatement.Filter(column, comparison: comparison);

            return this;
        }

        public QueryTemplate BuildTemplate()
        {
            while (this.statements.Any())
            {
                var statement = this.statements.Dequeue();
                statement.WriteTo(this.template);
            }

            this.template.Append(";");

            return QueryTemplate.Parse(this.template.ToString());
        }

        public QueryBuilder Delete()
        {
            this.BeginStatement(new DeleteStatement(this.Table));

            return this;
        }

        public QueryBuilder Insert(IEnumerable<EntityColumn> columns)
        {
            this.BeginStatement(new InsertStatement(this.Table));
            this.CurrentStatement.Select(columns);

            return this;
        }

        public void Reset()
        {
            this.template.Clear();
        }

        public QueryBuilder Select(IEnumerable<EntityColumn> columns)
        {
            this.BeginStatement(new SelectStatement(this.Table));
            this.CurrentStatement.Select(columns);

            return this;
        }

        public QueryBuilder OrderBy(EntityColumn column, SortDirection direction = SortDirection.Default)
        {
            this.BeginStatement(new OrderByStatement(this.Table));
            this.CurrentStatement.Sort(column, direction);

            return this;
        }

        public QueryBuilder OrderBy(IEnumerable<EntityColumn> columns)
        {
            this.BeginStatement(new OrderByStatement(this.Table));
            this.CurrentStatement.Sort(columns);

            return this;
        }

        public QueryBuilder Where(EntityColumn column)
        {
            this.BeginStatement(new WhereStatement(this.Table));
            this.CurrentStatement.Filter(column);

            return this;
        }

        public QueryBuilder Where(IEnumerable<EntityColumn> columns)
        {
            this.BeginStatement(new WhereStatement(this.Table));
            this.CurrentStatement.Filter(columns);

            return this;
        }

        protected QueryBuilder BeginStatement(QueryStatement statement)
        {
            if (statement.CanBeginStatement(this.CurrentStatement))
            {
                this.statements.Enqueue(statement);
                this.CurrentStatement = statement;

                return this;
            }

            throw new StatementException(this.CurrentStatement, statement);
        }
    }
}