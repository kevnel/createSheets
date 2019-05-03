using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using SmartSheetApiTest.TestData;

namespace SmartSheetApiTest.RestClient
{
    public class SmartSheetRestClient
    {
        private HttpClient _httpClient;
        private const string _apiToken = "sawxtwkk02gjbddko6mcgtl8mp";

        /// <summary>
        /// Instance of HttpClient to reduce number of sockets created.
        /// </summary>
        private HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                }
                return _httpClient;
            }
        }

        /// <summary>
        /// Constructor that sets base address and default request headers
        /// </summary>
        public SmartSheetRestClient()
        {
            // Set base address and default request headers
            HttpClient.BaseAddress = new Uri(" https://api.smartsheet.com/2.0/");
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiToken}");
        }

        /// <summary>
        /// Creates a sheet by calling the CreateSheet endpoint with the requested sheet data
        /// </summary>
        /// <param name="sheetToCreate">Representation of the sheet the user wants to create.</param>
        /// <returns>Response object containing StatusCode, Content (on OK status code) and Error Message (on non-OK status codes)</returns>
        public async Task<SmartSheetResponse<SmartSheetCreateResponse>> CreateSheet(CreateSmartSheetRequestObject sheetToCreate)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(sheetToCreate), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync("sheets", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            SmartSheetResponse< SmartSheetCreateResponse> sheetResponse = new SmartSheetResponse<SmartSheetCreateResponse>
            {
                StatusCode = response.StatusCode,
                Content = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<SmartSheetCreateResponse>(responseContent) : null,
                Error = response.StatusCode != HttpStatusCode.OK ? responseContent : null
            };
            return sheetResponse;
        }

        /// <summary>
        /// Reads the sheet data for the given sheet
        /// </summary>
        /// <param name="sheetId">Id for the sheet in question</param>
        /// <returns>Returns a subset of the information for the given sheet, or an error for non-200 status codes</returns>
        public async Task<SmartSheetResponse<SmartSheetResponseResult>> ReadSheet(double sheetId)
        {
            var response = await HttpClient.GetAsync($"sheets/{sheetId:R}");
            string responseContent = await response.Content.ReadAsStringAsync();
            SmartSheetResponse<SmartSheetResponseResult> sheetResponse = new SmartSheetResponse<SmartSheetResponseResult>
            {
                StatusCode = response.StatusCode,
                Content = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<SmartSheetResponseResult>(responseContent) : null,
                Error = response.StatusCode != HttpStatusCode.OK ? responseContent : null
            };
            return sheetResponse;
        }
    }
}
