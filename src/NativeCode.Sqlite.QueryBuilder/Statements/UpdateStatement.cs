namespace NativeCode.Sqlite.QueryBuilder.Statements
{
    using System;
    using System.Text;

    public class UpdateStatement : QueryStatement
    {
        public UpdateStatement(EntityTable table) : base("UPDATE", table)
        {
        }

        protected internal override void WriteTo(StringBuilder template)
        {
            throw new NotImplementedException();
        }
    }
}