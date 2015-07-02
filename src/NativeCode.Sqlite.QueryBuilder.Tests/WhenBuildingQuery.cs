﻿namespace NativeCode.Sqlite.QueryBuilder.Tests
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
            // Arrange, Act
            var template = QueryBuilder.From<Person>().Delete().BuildTemplate();

            // Assert
            Assert.AreEqual(this.GetExpectation(ExpectedDeleteQuery), template.Query);
        }

        [Test]
        public void ShouldBuildDeleteQueryFiltered()
        {
            // Arrange, Act
            var template = QueryBuilder.From<Person>().Delete().Where(person => person.GetPrimaryKey()).BuildTemplate();

            // Assert
            Assert.AreEqual(this.GetExpectation(ExpectedDeleteQueryFiltered), template.Query);
        }

        [Test]
        public void ShouldBuildSelectQuery()
        {
            // Arrange, Act
            var template = QueryBuilder.From<Person>().Select(person => person.Columns).BuildTemplate();

            // Assert
            Assert.AreEqual(this.GetExpectation(ExpectedSelectQuery), template.Query);
        }

        [Test]
        public void ShouldBuildSelectQueryFiltered()
        {
            // Arrange, Act
            var template =
                QueryBuilder.From<Person>()
                    .Select(person => person.Columns)
                    .Where(person => person.GetPrimaryKey())
                    .And(person => person["FirstName"])
                    .BuildTemplate();

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