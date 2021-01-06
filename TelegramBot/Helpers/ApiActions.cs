using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TelegramBot.Models;

namespace TelegramBot.Helpers
{
    /// <summary>
    /// Class with API requests
    /// </summary>
    public static class ApiActions
    {
        /// <summary>
        /// [GET] Returns a list of sampling points
        /// </summary>
        public static async Task<ResponseModel<SamplingPointModel>> GetSamplingPointsAsync()
        {
            var client = new RestClient(ApiUrls.BaseUrl);
            var request = new RestRequest(ApiUrls.GetSamplingPoints, Method.GET, DataFormat.Json);
            var response = await client.ExecuteAsync(request);
            var samplingPoints = JsonConvert.DeserializeObject<ResponseModel<SamplingPointModel>>(response.Content);

            return samplingPoints;
        }

        /// <summary>
        /// Returns a sampling point by name from the sampling points list
        /// </summary>
        /// <param name="name">Sampling point name</param>
        public static async Task<SamplingPointModel> GetSamplingPointByNameAsync(string name)
        {
            var samplingPointList = await GetSamplingPointsAsync();
            return samplingPointList.Data.FirstOrDefault(p => p.Name == name);
        }
    }
}