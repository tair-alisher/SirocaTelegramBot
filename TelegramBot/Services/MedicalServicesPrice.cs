using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services
{
    public static class MedicalServicesPrice
    {
        public static async Task SendMedicalServicesPrice(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Цены на услуги врачей\n");
            builder.AppendLine("Общий осмотр - 25р");
            builder.AppendLine("Окулист - 30р");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}