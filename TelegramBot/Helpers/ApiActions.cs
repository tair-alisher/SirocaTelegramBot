using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TelegramBot.Enums;
using TelegramBot.Exceptions;
using TelegramBot.Models;

namespace TelegramBot.Helpers
{
    /// <summary>
    /// Class with API requests
    /// </summary>
    public static class ApiActions
    {
        private static async Task<IRestResponse> ExecuteGetRequestAsync(string url)
        {
            var client = new RestClient(ApiUrls.BaseUrl);
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            var response = await client.ExecuteAsync(request);

            return response;
        }

        /// <summary>
        /// [GET] Returns information about the clinic
        /// </summary>
        public static async Task<string> GetClinicInformationAsync()
        {
            var apiUrl = $"{ApiUrls.GetInformation}{InformationType.BaseInfo.ToString()}";
            var response = await ExecuteGetRequestAsync(apiUrl);
            var information = JsonConvert.DeserializeObject<ResponseModel<string>>(response.Content);

            return information.Data;
        }

        /// <summary>
        /// [GET] Returns information about the covid19
        /// </summary>
        public static async Task<string> GetCovid19InformationAsync()
        {
            var apiUrl = $"{ApiUrls.GetInformation}{InformationType.Covid19Info.ToString()}";
            var response = await ExecuteGetRequestAsync(apiUrl);
            var information = JsonConvert.DeserializeObject<ResponseModel<string>>(response.Content);

            return information.Data;
        }

        /// <summary>
        /// [GET] Returns a list of sampling points
        /// </summary>
        public static async Task<IReadOnlyList<SamplingPointModel>> GetSamplingPointsAsync()
        {
            var response = await ExecuteGetRequestAsync(ApiUrls.GetSamplingPoints);
            var samplingPoints = JsonConvert.DeserializeObject<ResponseModel<List<SamplingPointModel>>>(response.Content);

            return samplingPoints.Data;
        }

        /// <summary>
        /// [GET] Returns a sampling point by name from the sampling points list
        /// </summary>
        /// <param name="name">Sampling point name</param>
        public static async Task<SamplingPointModel> GetSamplingPointByNameAsync(string name)
        {
            var samplingPointList = await GetSamplingPointsAsync();
            return samplingPointList.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        /// Returns a list of clinic laboratory services
        /// </summary>
        public static async Task<IReadOnlyList<ServiceInfoModel>> GetLaboratoryServices()
        {
            var response = await ExecuteGetRequestAsync(ApiUrls.GetLaboratoryServicesPriceList);
            var services =
                JsonConvert.DeserializeObject<ResponseModel<IReadOnlyList<ServiceInfoModel>>>(response.Content);

            return services.Data;
        }

        /// <summary>
        /// Returns a list of clinic medical services
        /// </summary>
        public static async Task<IReadOnlyList<ServiceInfoModel>> GetMedicalServices()
        {
            var response = await ExecuteGetRequestAsync(ApiUrls.GetMedicalServicesPriceList);
            var services =
                JsonConvert.DeserializeObject<ResponseModel<IReadOnlyList<ServiceInfoModel>>>(response.Content);

            return services.Data;
        }

        /// <summary>
        /// Sends user's phone number and
        /// </summary>
        /// <param name="phoneNumber">User's phone number</param>
        /// <param name="category">Category type</param>
        public static async Task<bool> SendPhoneNumber(string phoneNumber, ServiceType category)
        {
            var postFeedBackRequest = string.Format(ApiUrls.PostFeedBackRequest, phoneNumber, category.ToString());

            var client = new RestClient(ApiUrls.BaseUrl);
            var request = new RestRequest(postFeedBackRequest, Method.POST, DataFormat.Json);
            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ResponseModel<string>>(response.Content).IsSuccess;
        }

        /// <summary>
        /// Returns Session_id value
        /// </summary>
        public static async Task<string> RequestSessionId()
        {
            var response = await ExecuteGetRequestAsync(ApiUrls.AuthWebApiUser);
            var apiUser = JsonConvert.DeserializeObject<ResponseModel<ApiUserModel>>(response.Content);

            return !apiUser.IsSuccess ? string.Empty : apiUser.Data.SessionId;
        }

        /// <summary>
        /// Returns test results pdf
        /// </summary>
        /// <param name="sessionId">Session id</param>
        /// <param name="codeWord">Codeword</param>
        public static async Task<Stream> GetTestResults(string sessionId, string codeWord)
        {
            var getResultsByCodeWordAsPdfUrl = string.Format(ApiUrls.GeResultsByCodeWordAsPdf, codeWord);

            var client = new RestClient(ApiUrls.BaseUrl);
            var request = new RestRequest(getResultsByCodeWordAsPdfUrl, Method.GET);
            request.AddHeader("Session_id", sessionId);

            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new TestResultsPdfNotFound();

            return new MemoryStream(client.DownloadData(request));
        }
    }
}