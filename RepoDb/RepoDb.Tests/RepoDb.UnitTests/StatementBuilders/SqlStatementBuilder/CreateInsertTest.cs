﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.StatementBuilders;
using RepoDb.UnitTests.Setup;
using System;

namespace RepoDb.UnitTests.StatementBuilders
{
    [TestClass]
    public class SqlStatementBuilderCreateInsertTest
    {
        [TestMethod]
        public void TestSqlStatementBuilderCreateInsert()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: null);
            var expected = $"" +
                $"INSERT INTO [Table] " +
                $"( [Field1], [Field2], [Field3] ) " +
                $"VALUES " +
                $"( @Field1, @Field2, @Field3 ) ; " +
                $"SELECT NULL AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSqlStatementBuilderCreateInsertWithQuotedTableSchema()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "[dbo].[Table]";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: null);
            var expected = $"" +
                $"INSERT INTO [dbo].[Table] " +
                $"( [Field1], [Field2], [Field3] ) " +
                $"VALUES " +
                $"( @Field1, @Field2, @Field3 ) ; " +
                $"SELECT NULL AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSqlStatementBuilderCreateInsertWithUnquotedTableSchema()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "dbo.Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: null);
            var expected = $"" +
                $"INSERT INTO [dbo].[Table] " +
                $"( [Field1], [Field2], [Field3] ) " +
                $"VALUES " +
                $"( @Field1, @Field2, @Field3 ) ; " +
                $"SELECT NULL AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSqlStatementBuilderCreateInsertWithPrimary()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var primaryField = new DbField("Field1", true, false, false, typeof(int), null, null, null, null, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: primaryField,
                identityField: null);
            var expected = $"" +
                $"INSERT INTO [Table] " +
                $"( [Field1], [Field2], [Field3] ) " +
                $"VALUES " +
                $"( @Field1, @Field2, @Field3 ) ; " +
                $"SELECT @Field1 AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSqlStatementBuilderCreateInsertWithIdentity()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var identityField = new DbField("Field1", false, true, false, typeof(int), null, null, null, null, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: identityField);
            var expected = $"" +
                $"INSERT INTO [Table] " +
                $"( [Field2], [Field3] ) " +
                $"VALUES " +
                $"( @Field2, @Field3 ) ; " +
                $"SELECT CONVERT(INT, SCOPE_IDENTITY()) AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSqlStatementBuilderCreateInsertWithIdentityAsBigInt()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var identityField = new DbField("Field1", false, true, false, typeof(long), null, null, null, null, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: identityField);
            var expected = $"" +
                $"INSERT INTO [Table] " +
                $"( [Field2], [Field3] ) " +
                $"VALUES " +
                $"( @Field2, @Field3 ) ; " +
                $"SELECT CONVERT(BIGINT, SCOPE_IDENTITY()) AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSqlStatementBuilderCreateInsertWithPrimaryAndIdentity()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var primaryField = new DbField("Field1", true, false, false, typeof(int), null, null, null, null, Helper.DbSetting);
            var identityField = new DbField("Field2", false, true, false, typeof(int), null, null, null, null, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: identityField);
            var expected = $"" +
                $"INSERT INTO [Table] " +
                $"( [Field1], [Field3] ) " +
                $"VALUES " +
                $"( @Field1, @Field3 ) ; " +
                $"SELECT CONVERT(INT, SCOPE_IDENTITY()) AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSqlStatementBuilderCreateInsertWithPrimaryAndIdentityAsBigInt()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var primaryField = new DbField("Field1", true, false, false, typeof(int), null, null, null, null, Helper.DbSetting);
            var identityField = new DbField("Field2", false, true, false, typeof(long), null, null, null, null, Helper.DbSetting);

            // Act
            var actual = statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: identityField);
            var expected = $"" +
                $"INSERT INTO [Table] " +
                $"( [Field1], [Field3] ) " +
                $"VALUES " +
                $"( @Field1, @Field3 ) ; " +
                $"SELECT CONVERT(BIGINT, SCOPE_IDENTITY()) AS [Result] ;";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void ThrowExceptionOnSqlStatementBuilderCreateInsertIfTheNonIdentityPrimaryIsNotCovered()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var primaryField = new DbField("Id", true, false, false, typeof(int), null, null, null, null, Helper.DbSetting);

            // Act
            statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: primaryField,
                identityField: null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnSqlStatementBuilderCreateInsertIfThereAreNoFields()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";

            // Act
            statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: null,
                primaryField: null,
                identityField: null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnSqlStatementBuilderCreateInsertIfTheTableIsNull()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = (string)null;

            // Act
            statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: null,
                primaryField: null,
                identityField: null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnSqlStatementBuilderCreateInsertIfTheTableIsEmpty()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "";

            // Act
            statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: null,
                primaryField: null,
                identityField: null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnSqlStatementBuilderCreateInsertIfTheTableIsWhitespace()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = " ";

            // Act
            statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: null,
                primaryField: null,
                identityField: null);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void ThrowExceptionOnSqlStatementBuilderCreateInsertIfThePrimaryIsNotReallyAPrimary()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var primaryField = new DbField("Field1", false, false, false, typeof(int), null, null, null, null, Helper.DbSetting);

            // Act
            statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: primaryField,
                identityField: null);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void ThrowExceptionOnSqlStatementBuilderCreateInsertIfTheIdentityIsNotReallyAnIdentity()
        {
            // Setup
            var statementBuilder = new SqlServerStatementBuilder();
            var queryBuilder = new QueryBuilder();
            var tableName = "Table";
            var fields = Field.From(new[] { "Field1", "Field2", "Field3" }, Helper.DbSetting);
            var qualifiers = Field.From("Field1", Helper.DbSetting);
            var identifyField = new DbField("Field2", false, false, false, typeof(int), null, null, null, null, Helper.DbSetting);

            // Act
            statementBuilder.CreateInsert(queryBuilder: queryBuilder,
                tableName: tableName,
                fields: fields,
                primaryField: null,
                identityField: identifyField);
        }
    }
}
