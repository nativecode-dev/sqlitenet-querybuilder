namespace NativeCode.Sqlite.QueryBuilder.Tests
{
    using NativeCode.Sqlite.QueryBuilder.TemplateBuilders;
    using NativeCode.Sqlite.QueryBuilder.Tests.Entities;

    using NUnit.Framework;

    [TestFixture]
    public class WhenBuildingQuery
    {
        [Test]
        public void BuildQueries()
        {
            var configuration = new QueryBuilderConfiguration { QualifyColumnNames = true, QualifyTableNames = true, StoreDateTimeAsTicks = true };
            QueryBuilder.Configuration = configuration;

            var table = QueryBuilderCache.GetEntityTable<Person>();

            var delete = new DeleteQueryTemplateBuilder(table);
            var deleteTemplate = delete.BuildTemplate();

            var insert = new InsertQueryTemplateBuilder(table);
            var insertTemplate = insert.BuildTemplate();

            var select = new SelectQueryTemplateBuilder(table);
            var selectTemplate = select.BuildTemplate();

            var update = new UpdateQueryTemplateBuilder(table);
            var updateTemplate = update.BuildTemplate();

            Assert.IsNotEmpty(deleteTemplate.Query);
            Assert.IsNotEmpty(insertTemplate.Query);
            Assert.IsNotEmpty(selectTemplate.Query);
            Assert.IsNotEmpty(updateTemplate.Query);
        }
    }
}