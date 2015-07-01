namespace NativeCode.Sqlite.QueryBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using SQLite.Net.Attributes;

    public class EntityTable
    {
        private readonly List<EntityColumn> columns;

        public EntityTable(Type type)
        {
            this.Type = type;

            var properties = type.GetRuntimeProperties().ToList();
            this.columns = new List<EntityColumn>(properties.Count);

            foreach (var property in properties)
            {
                this.columns.Add(new EntityColumn(property));
            }

            this.Alias = this.GetTableName();
            this.Name = this.GetTableName();
        }

        public IReadOnlyList<EntityColumn> Columns
        {
            get { return this.columns; }
        }

        public string Alias { get; private set; }

        public string Name { get; private set; }

        public Type Type { get; private set; }

        public string GetTableName()
        {
            var attribute = this.Type.GetTypeInfo().GetCustomAttribute<TableAttribute>();

            if (attribute != null && attribute.Name != null)
            {
                return attribute.Name;
            }

            return this.Type.Name;
        }
    }
}