namespace TelegramBot.Helpers
{
    /// <summary>
    /// Class with API requests urls
    /// </summary>
    public static class ApiUrls
    {
        private static string _baseUrl;
        /// <summary>
        /// Base api address
        /// </summary>
        public static string BaseUrl {
            get
            {
                if (!string.IsNullOrEmpty(_baseUrl))
                    return _baseUrl;

                _baseUrl = Settings.BotSettings.ApiBaseUrl;
                return _baseUrl;
            }
        }

        /// <summary>
        /// Url to request information about the clinic
        /// </summary>
        public const string GetInformation = "api/Auxiliary/GetDictInfo?request=1&key=";

        /// <summary>
        /// Url to request a price list of laboratory services
        /// </summary>
        public const string GetLaboratoryServicesPriceList = "api/Appointment/GetServices?request=1&islabserv=true";

        /// <summary>
        /// Url to request a price list of medical services
        /// </summary>
        public const string GetMedicalServicesPriceList = "api/Appointment/GetServices?request=1&islabserv=false";

        /// <summary>
        /// Url to request information about sampling points
        /// </summary>
        public const string GetSamplingPoints = "api/Auxiliary/GetSamplingPoints?request=1&key=1";

        /// <summary>
        /// Url to send phone number
        /// </summary>
        public const string PostFeedBackRequest =
            "api/Auxiliary/PostFeedBackRequest?request=1&phoneNo={0}&category={1}";

        /// <summary>
        /// Url to request Session_id value
        /// </summary>
        public const string AuthWebApiUser = "api/User/AuthWebApiUser";

        /// <summary>
        /// Url to request test results
        /// </summary>
        public const string GeResultsByCodeWordAsPdf = "api/Appointment/GetResultsByCodeWordAsPdf?codeWord={0}";
    }
}