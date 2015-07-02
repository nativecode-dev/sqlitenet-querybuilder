namespace NativeCode.Sqlite.QueryBuilder.Statements
{
    using System;
    using System.Text;

    public class SetStatement : QueryStatement
    {
        public SetStatement(EntityTable table) : base("SET", table)
        {
        }

        public override bool CanBeginStatement(QueryStatement current)
        {
            return current is OrderByStatement;
        }

        protected internal override void WriteTo(StringBuilder template)
        {
            throw new NotImplementedException();
        }
    }
}