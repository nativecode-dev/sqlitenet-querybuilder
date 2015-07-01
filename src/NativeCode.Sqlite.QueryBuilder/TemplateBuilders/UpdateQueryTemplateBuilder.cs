namespace NativeCode.Sqlite.QueryBuilder.TemplateBuilders
{
    using System.Linq;

    using NativeCode.Sqlite.QueryBuilder.Extensions;

    public class UpdateQueryTemplateBuilder : QueryTemplateBuilder
    {
        public UpdateQueryTemplateBuilder(EntityTable table) : base(table)
        {
        }

        protected override void BuildQuery()
        {
            var columns = from column in this.Table.GetUpdatableColumns()
                          let key = this.GetColumnName(column)
                          let value = this.GetColumnValue(column)
                          select key + " = " + value;

            this.Template.AppendLine("UPDATE " + this.GetTableName(this.Table));
            this.Template.AppendLine("SET " + columns.Join());
            this.Template.Append(this.GetWhereByPrimaryKeys(this.Table));
            this.Template.Append(";");
        }
    }
}