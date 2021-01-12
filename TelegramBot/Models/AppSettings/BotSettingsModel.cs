namespace TelegramBot.Models.AppSettings
{
    public class BotSettingsModel
    {
        public string Token { get; set; }
        public string BotDescription { get; set; }
        public string HelpDescription { get; set; }
        public string ApiBaseUrl { get; set; }
    }
}