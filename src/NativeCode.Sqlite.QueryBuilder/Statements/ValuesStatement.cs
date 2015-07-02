namespace NativeCode.Sqlite.QueryBuilder.Statements
{
    using System;
    using System.Text;

    public class ValuesStatement : QueryStatement
    {
        public ValuesStatement(EntityTable table) : base("VALUES", table)
        {
        }

        public override bool CanBeginStatement(QueryStatement current)
        {
            return current is InsertStatement;
        }

        protected internal override void WriteTo(StringBuilder template)
        {
            throw new NotImplementedException();
        }
    }
}