namespace FlightLogNet.Tests.Operation
{
    using System;
    using System.Text;

    using FlightLogNet.Operation;

    using Xunit;

    public class GetExportToCsvOperationTests
    {
        private readonly GetExportToCsvOperation getExportToCsvOperation;

        public GetExportToCsvOperationTests(GetExportToCsvOperation getExportToCsvOperation)
        {
            this.getExportToCsvOperation = getExportToCsvOperation;
        }

        // TODO 6.1: Odstra�t� skip a doplnt� test, aby otestoval vr�cen� CSV soubor.
        [Fact(Skip = "Not implemented.")]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            var result = getExportToCsvOperation.Execute();

            // Assert
            //Assert.Equal(expectedCsv, result);
        }
    }
}