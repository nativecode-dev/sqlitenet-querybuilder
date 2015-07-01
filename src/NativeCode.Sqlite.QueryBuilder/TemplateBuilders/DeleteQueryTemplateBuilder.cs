namespace NativeCode.Sqlite.QueryBuilder.TemplateBuilders
{
    public class DeleteQueryTemplateBuilder : QueryTemplateBuilder
    {
        public DeleteQueryTemplateBuilder(EntityTable table) : base(table)
        {
        }

        protected override void BuildQuery()
        {
            this.Template.AppendLine("DELETE FROM " + this.GetTableName(this.Table));
            this.Template.Append(this.GetWhereByPrimaryKeys(this.Table));
            this.Template.Append(";");
        }
    }
}