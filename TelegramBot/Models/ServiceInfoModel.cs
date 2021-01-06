namespace TelegramBot.Models
{
    /// <summary>
    /// Model for clinic service information
    /// </summary>
    public class ServiceInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SpecialityName { get; set; }
        public int SpecialityId { get; set; }
        public int TimeMin { get; set; }

        public string Information => $"{Name} - {Price} ({SpecialityName})";
    }
}