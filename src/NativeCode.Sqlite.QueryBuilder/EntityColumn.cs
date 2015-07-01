namespace NativeCode.Sqlite.QueryBuilder
{
    using System.Reflection;

    using NativeCode.Sqlite.QueryBuilder.Converters;

    using SQLite.Net.Attributes;

    public class EntityColumn
    {
        public EntityColumn(PropertyInfo property)
        {
            this.Property = property;

            this.Alias = this.GetColumnName();
            this.Name = this.GetColumnName();
        }

        public string Alias { get; private set; }

        public IQueryValueConverter Converter { get; set; }

        public string Name { get; private set; }

        public PropertyInfo Property { get; private set; }

        public string GetColumnName()
        {
            var attribute = this.Property.GetCustomAttribute<ColumnAttribute>();

            if (attribute != null && attribute.Name != null)
            {
                return attribute.Name;
            }

            return this.Property.Name;
        }
    }
}