using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace SmartSheetApiTest.TestData
{
    /// <summary>
    /// CreateSheet and ReadSheet return different objects, so this is used to wrap both. 
    /// </summary>
    /// <typeparam name="T">Should either be SmartSheetCreateResponse (CreateSheet) or SmartSheetResponseResult (ReadSheet)</typeparam>
    public class SmartSheetResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Content { get; set; }
        public string Error { get; set; }
    }

    /// <summary>
    /// CreateSheet endpoint wraps the actual sheet data in this response object.
    /// </summary>
    public class SmartSheetCreateResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("resultCode")]
        public int ResultCode { get; set; }
        [JsonProperty("result")]
        public SmartSheetResponseResult Result { get; set; }

        public SmartSheetCreateResponse()
        {
            Result = new SmartSheetResponseResult();
        }
        
    }

    /// <summary>
    /// ReadSheet returns this object, and CreateSheet returns this wrapped inside of SmartSheetCreateResponse.
    /// </summary>
    public class SmartSheetResponseResult
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public double Id { get; set; }
        [JsonProperty("permalink")]
        public string Permalink { get; set; }
        [JsonProperty("accessLevel")]
        public string AccessLevel { get; set; }
        [JsonProperty("columns")]
        public List<SmartSheetResponseColumn> Columns { get; set; }

        public SmartSheetResponseResult()
        {
            Columns = new List<SmartSheetResponseColumn>();
        }
    }

    /// <summary>
    /// Represents the column data in sheets. There can be multiple columns per sheet. 
    /// </summary>
    public class SmartSheetResponseColumn
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("primary")]
        public bool Primary { get; set; }
        [JsonProperty("id")]
        public bool Id { get; set; }
        [JsonProperty("index")]
        public bool Index { get; set; }
    }
}
