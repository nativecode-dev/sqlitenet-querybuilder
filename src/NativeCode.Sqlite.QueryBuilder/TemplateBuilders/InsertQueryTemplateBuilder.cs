namespace NativeCode.Sqlite.QueryBuilder.TemplateBuilders
{
    using System.Linq;

    using NativeCode.Sqlite.QueryBuilder.Extensions;

    public class InsertQueryTemplateBuilder : QueryTemplateBuilder
    {
        public InsertQueryTemplateBuilder(EntityTable table) : base(table)
        {
        }

        protected override void BuildQuery()
        {
            this.Template.AppendLine("INSERT INTO " + this.GetTableName(this.Table));
            this.Template.AppendLine("(" + this.Table.GetUpdatableColumns().Select(this.GetColumnName).Join() + ")");
            this.Template.AppendLine("VALUES");
            this.Template.Append("(" + this.Table.GetUpdatableColumns().Select(this.GetColumnValue).Join() + ")");
            this.Template.Append(";");
        }
    }
}