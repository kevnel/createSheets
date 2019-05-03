using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartSheetApiTest.TestData
{
    /// <summary>
    /// Represents the complete input for the CreateSheet endpoint
    /// </summary>
    public class CreateSmartSheetRequestObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("columns")]
        public List<CreateSmartSheetRequestColumn> Columns { get; set; }

        public CreateSmartSheetRequestObject()
        {
            Columns = new List<CreateSmartSheetRequestColumn>();
        }
    }

    /// <summary>
    /// Represents a column within a sheet. Can have many of these in a single sheet.
    /// </summary>
    public class CreateSmartSheetRequestColumn
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("primary")]
        public bool Primary { get; set; }
    }
}
