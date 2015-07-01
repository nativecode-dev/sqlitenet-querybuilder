namespace NativeCode.Sqlite.QueryBuilder.TemplateBuilders
{
    using System.Linq;

    public class SelectQueryTemplateBuilder : QueryTemplateBuilder
    {
        public SelectQueryTemplateBuilder(EntityTable table) : base(table)
        {
        }

        protected override void BuildQuery()
        {
            this.Template.AppendLine("SELECT");
            this.Template.AppendLine(string.Join(", ", this.Table.GetAllColumns().Select(this.GetColumnName)));
            this.Template.Append(this.GetWhereByPrimaryKeys(this.Table));
            this.Template.Append(";");
        }
    }
}