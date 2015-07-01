namespace NativeCode.Sqlite.QueryBuilder.Converters
{
    using System;
    using System.Linq;

    public sealed class ConvertAttribute : Attribute
    {
        public ConvertAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }

        public IQueryValueConverter GetConverter()
        {
            return QueryBuilder.Current.Converters.FirstOrDefault(x => x.CanConvert(this.Type));
        }
    }
}