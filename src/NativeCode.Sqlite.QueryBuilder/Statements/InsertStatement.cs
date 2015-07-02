namespace NativeCode.Sqlite.QueryBuilder.Statements
{
    using System;
    using System.Text;

    public class InsertStatement : QueryStatement
    {
        protected internal InsertStatement(EntityTable table) : base("INSERT", table)
        {
        }

        protected internal override void WriteTo(StringBuilder template)
        {
            throw new NotImplementedException();
        }
    }
}