using System.Globalization;

namespace TelegramBot.Models
{
    public class SamplingPointModel
    {
        /// <summary>
        /// Sampling point name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sampling point latitude and longitude values in format "latitude longitude"
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Latitude value extracted from Location property
        /// </summary>
        public float Latitude => float.Parse(
            Location.Split(',')[0].Trim(),
            CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// Longitude value extracted from Location property
        /// </summary>
        public float Longitude => float.Parse(
            Location.Split(',')[1].Trim(),
            CultureInfo.InvariantCulture.NumberFormat);
    }
}