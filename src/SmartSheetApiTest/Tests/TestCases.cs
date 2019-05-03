using NUnit.Framework;
using SmartSheetApiTest.TestData;
using SmartSheetApiTest.RestClient;
using System.Net;
using System.Threading;

namespace SmartSheetApiTest.Tests
{
    [TestFixture]
    public class TestCases
    {
        [Test]
        public void CreateSheet_ValidateContents()
        {
            SmartSheetRestClient client = new SmartSheetRestClient();

            // Set up SmartSheet request object
            CreateSmartSheetRequestObject sheet = new CreateSmartSheetRequestObject();
            sheet.Name = "Kevin Test Sheet";
            sheet.Columns.Add(new CreateSmartSheetRequestColumn
            {
                Title = "favorite",
                Type = ColumnTypes.CHECKBOX.ToString(),
                Symbol = "STAR"
            });
            sheet.Columns.Add(new CreateSmartSheetRequestColumn
            {
                Title = "Primary Column",
                Type = ColumnTypes.TEXT_NUMBER.ToString(),
                Primary = true
            });

            // Create the SmartSheet
            SmartSheetResponse<SmartSheetCreateResponse> createResponse = client.CreateSheet(sheet).Result;

            // Verify that the create request was successful
            Assert.AreEqual(HttpStatusCode.OK, createResponse.StatusCode, "Create sheet returned a non-200 response.");
            VerificationHelpers.CreateSheetValidation(sheet, createResponse.Content.Result);

            // Get Sheet from endpoint and compare data
            // I noticed that there are instances where the Read would return NotFound, but in my investigation of these instances, the sheet was created eventually.
            // Adding this retry as a quick fix. Normally I would investigate this further to see if there were better ways to handle this. 
            SmartSheetResponse<SmartSheetResponseResult> readResponse = null;
            for (int i = 0; i < 3; i++)
            {
                readResponse = client.ReadSheet(createResponse.Content.Result.Id).Result;
                if (readResponse.StatusCode == HttpStatusCode.OK)
                {
                    break;
                }
                Thread.Sleep(1000);
            }

            // Verify that the read request was successful
            Assert.AreEqual(HttpStatusCode.OK, readResponse.StatusCode, $"Read response for sheet {createResponse.Content.Result.Id.ToString()} returned a non-200 response.");
            VerificationHelpers.CompareCreatedVsRead(createResponse.Content.Result, readResponse.Content);
        }
    }
}
