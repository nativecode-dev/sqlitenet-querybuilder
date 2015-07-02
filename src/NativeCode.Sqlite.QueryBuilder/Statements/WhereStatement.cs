namespace NativeCode.Sqlite.QueryBuilder.Statements
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    using NativeCode.Sqlite.QueryBuilder.Extensions;

    public class WhereStatement : QueryStatement
    {
        private static readonly Type[] AllowedStatements =
        {
            typeof(DeleteStatement), typeof(InsertStatement), typeof(OrderByStatement), typeof(SelectStatement),
            typeof(SetStatement), typeof(UpdateStatement), typeof(ValuesStatement)
        };

        public WhereStatement(EntityTable table) : base("WHERE", table)
        {
        }

        public override bool CanBeginStatement(QueryStatement current)
        {
            return AllowedStatements.Contains(current.GetType());
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        protected internal override void WriteTo(StringBuilder template)
        {
            template.AppendLine();
            template.Append(this.Keyword);
            template.Append(Space);
            template.Append(this.GetFilterString());
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private string GetFilterString()
        {
            var result = string.Empty;

            for (var index = 0; index < this.Filters.Count; index++)
            {
                var filter = this.Filters[index];
                var last = index == this.Filters.Count - 1;

                result += filter.Column.GetName(false) + Space + filter.Comparison.Stringify();

                if (filter.Comparison != FilterComparison.IsNotNull && filter.Comparison != FilterComparison.IsNull)
                {
                    var value = filter.Column.Tokenize();

                    if (filter.Column.IsQuotable)
                    {
                        value = value.QuoteString();
                    }

                    result += Space + value;
                }

                if (!last)
                {
                    result += Space + filter.Expression.Stringify() + Space;
                }
            }

            return result;
        }
    }
}