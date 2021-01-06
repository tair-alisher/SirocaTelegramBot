namespace TelegramBot.Helpers
{
    /// <summary>
    /// Class with API requests urls
    /// </summary>
    public static class ApiUrls
    {
        /// <summary>
        /// Base api address
        /// </summary>
        public const string BaseUrl = "http://siroca.com:2122";

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
    }
}