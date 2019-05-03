using NUnit.Framework;
using SmartSheetApiTest.TestData;

namespace SmartSheetApiTest.Tests
{
    public static class VerificationHelpers
    {
        /// <summary>
        /// Validates a created sheet against the input parameters to the CreateSheet method
        /// </summary>
        /// <param name="expected">Input parameters into the CreateSheet method</param>
        /// <param name="actual">Output from the CreateSheet method</param>
        public static void CreateSheetValidation(CreateSmartSheetRequestObject expected, SmartSheetResponseResult actual)
        {
            Assert.AreEqual(expected.Name, actual.Name, "Sheet name is different between input parameters and createSheet response.");
            Assert.AreEqual(expected.Columns.Count, actual.Columns.Count, "Sheet column count is different between input parameters and createSheet response.");
            // Making the assumption that the columns are created in the order they were listed.
            Assert.AreEqual(expected.Columns[0].Title, actual.Columns[0].Title, "Sheet column title is different between input parameters and createSheet response.");
            Assert.AreEqual(expected.Columns[0].Type, actual.Columns[0].Type, "Sheet column type is different between input parameters and createSheet response.");
            Assert.AreEqual(expected.Columns[0].Symbol, actual.Columns[0].Symbol, "Sheet column symbol is different between input parameters and createSheet response.");
            Assert.AreEqual(expected.Columns[1].Title, actual.Columns[1].Title, "Sheet column title is different between input parameters and createSheet response.");
            Assert.AreEqual(expected.Columns[1].Type, actual.Columns[1].Type, "Sheet column type is different between input parameters and createSheet response.");
            Assert.AreEqual(expected.Columns[1].Primary, actual.Columns[1].Primary, "Sheet column primary is different between input parameters and createSheet response.");
        }

        /// <summary>
        /// Validates a created sheet against the data returned from reading that sheet
        /// </summary>
        /// <param name="expected">Output from the CreateSheet method</param>
        /// <param name="actual">Output from the ReadSheet method</param>
        public static void CompareCreatedVsRead(SmartSheetResponseResult expected, SmartSheetResponseResult actual)
        {
            Assert.AreEqual(expected.Name, actual.Name, "Sheet name is different between CreateSheet response and ReadSheet response.");
            Assert.AreEqual(expected.Columns.Count, actual.Columns.Count, "Sheet column count is different between CreateSheet response and ReadSheet response.");
            // Making the assumption that the columns are created in the order they were listed.
            Assert.AreEqual(expected.Columns[0].Title, actual.Columns[0].Title, "Sheet column title is different between CreateSheet response and ReadSheet response.");
            Assert.AreEqual(expected.Columns[0].Type, actual.Columns[0].Type, "Sheet column type is different between CreateSheet response and ReadSheet response.");
            Assert.AreEqual(expected.Columns[0].Symbol, actual.Columns[0].Symbol, "Sheet column symbol is different between CreateSheet response and ReadSheet response.");
            Assert.AreEqual(expected.Columns[1].Title, actual.Columns[1].Title, "Sheet column title is different between CreateSheet response and ReadSheet response.");
            Assert.AreEqual(expected.Columns[1].Type, actual.Columns[1].Type, "Sheet column type is different between CreateSheet response and ReadSheet response.");
            Assert.AreEqual(expected.Columns[1].Primary, actual.Columns[1].Primary, "Sheet column primary is different between CreateSheet response and ReadSheet response.");
        }
    }
}
