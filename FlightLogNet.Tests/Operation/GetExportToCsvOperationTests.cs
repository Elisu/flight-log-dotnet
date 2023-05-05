using System.IO;
using System.Reflection;

namespace FlightLogNet.Tests.Operation
{
    using System;
    using System.Text;

    using FlightLogNet.Operation;

    using Xunit;

    using Microsoft.Extensions.Configuration;

    public class GetExportToCsvOperationTests
    {
        private readonly GetExportToCsvOperation getExportToCsvOperation;
        private readonly IConfiguration configuration;

        public GetExportToCsvOperationTests(GetExportToCsvOperation getExportToCsvOperation, IConfiguration configuration)
        {
            this.getExportToCsvOperation = getExportToCsvOperation;
            this.configuration = configuration;
        }

        [Fact]
        public void Execute_ShouldCreateReportCsv()
        {
            // Arrange
            TestDatabaseGenerator.DeleteOldDatabase(this.configuration);
            DateTime fixedDate = new DateTime(2020, 1, 2, 16, 57, 10);
            TestDatabaseGenerator.CreateTestDatabaseWithFixedTime(fixedDate, this.configuration);
            var expectedCsv = GetTestCsv();

            // Act
            var result = getExportToCsvOperation.Execute();

            // Assert
            Assert.Equal(expectedCsv, result);
        }

        private static byte[] GetTestCsv()
        {
            using var reader =
                new StreamReader($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\..\export.csv");
            return Encoding.UTF8.GetBytes(reader.ReadToEnd());
        }
    }
}
