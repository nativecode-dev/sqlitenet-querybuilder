﻿namespace NativeCode.Sqlite.QueryBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Freezable;

    using NativeCode.Sqlite.QueryBuilder.Converters;

    public class QueryBuilderConfiguration : IFreezable
    {
        public static readonly QueryBuilderConfiguration Default = new QueryBuilderConfiguration(true);

        private bool isFrozen;

        public QueryBuilderConfiguration()
        {
            this.Converters = new List<IQueryValueConverter> { new DateTimeConverter() };
        }

        private QueryBuilderConfiguration(bool frozen) : this()
        {
            this.isFrozen = frozen;
        }

        public bool AlwaysUseColumnAlias { get; set; }

        public bool AlwaysUseTableAlias { get; set; }

        public List<IQueryValueConverter> Converters { get; private set; }

        [SuppressMessage("ReSharper", "ConvertToAutoPropertyWithPrivateSetter", Justification = "Reviewed. Suppression is OK here. Freezable.")]
        public bool IsFrozen
        {
            get { return this.isFrozen; }
        }

        public bool QualifyColumnNames { get; set; }

        public bool QualifyTableNames { get; set; }

        public bool StoreDateTimeAsTicks { get; set; }

        public void Freeze()
        {
            this.isFrozen = true;
        }
    }
}