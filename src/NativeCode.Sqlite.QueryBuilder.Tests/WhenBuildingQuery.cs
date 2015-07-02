namespace NativeCode.Sqlite.QueryBuilder.Tests
{
    using System.IO;

    using NativeCode.Sqlite.QueryBuilder.Tests.Entities;

    using NUnit.Framework;

    [TestFixture]
    public class WhenBuildingQuery
    {
        private const string ExpectedDeleteQuery = "NativeCode.Sqlite.QueryBuilder.Tests.Expectations.DeleteQuery.txt";

        private const string ExpectedDeleteQueryFiltered = "NativeCode.Sqlite.QueryBuilder.Tests.Expectations.DeleteQueryFiltered.txt";

        private const string ExpectedSelectQuery = "NativeCode.Sqlite.QueryBuilder.Tests.Expectations.SelectQuery.txt";

        private const string ExpectedSelectQueryFiltered = "NativeCode.Sqlite.QueryBuilder.Tests.Expectations.SelectQueryFiltered.txt";

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            var configuration = new QueryBuilderConfiguration
            {
                QualifyColumnNames = true,
                QualifyTableNames = true,
                StoreDateTimeAsTicks = true,
                UseColumnAlias = true,
                UseTableAlias = true
            };

            QueryBuilderConfiguration.Current = configuration;
        }

        [Test]
        public void ShouldBuildDeleteQuery()
        {
            // Arrange
            var table = QueryBuilderCache.GetEntityTable<Person>();

            // Act
            var template = QueryBuilder.New(table).Delete().BuildTemplate();

            // Assert
            Assert.AreEqual(this.GetExpectation(ExpectedDeleteQuery), template.Query);
        }

        [Test]
        public void ShouldBuildDeleteQueryFiltered()
        {
            // Arrange
            var table = QueryBuilderCache.GetEntityTable<Person>();

            // Act
            var template = QueryBuilder.New(table).Delete().Where(table.GetPrimaryKey()).BuildTemplate();

            // Assert
            Assert.AreEqual(this.GetExpectation(ExpectedDeleteQueryFiltered), template.Query);
        }

        [Test]
        public void ShouldBuildSelectQuery()
        {
            // Arrange
            var table = QueryBuilderCache.GetEntityTable<Person>();

            // Act
            var template = QueryBuilder.New(table).Select(table.Columns).BuildTemplate();

            // Assert
            Assert.AreEqual(this.GetExpectation(ExpectedSelectQuery), template.Query);
        }

        [Test]
        public void ShouldBuildSelectQueryFiltered()
        {
            // Arrange
            var table = QueryBuilderCache.GetEntityTable<Person>();

            // Act
            var template = QueryBuilder.New(table).Select(table.Columns).Where(table.GetPrimaryKey()).And(table["FirstName"]).BuildTemplate();

            // Assert
            Assert.AreEqual(this.GetExpectation(ExpectedSelectQueryFiltered), template.Query);
        }

        private string GetExpectation(string key)
        {
            var assembly = this.GetType().Assembly;

            // ReSharper disable once AssignNullToNotNullAttribute
            using (var reader = new StreamReader(assembly.GetManifestResourceStream(key)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}