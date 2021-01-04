using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services
{
    public static class LaboratoryServicesPrice
    {
        public static async Task SendLaboratoryServicesPrice(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Цены на услуги лаборатории\n");
            builder.AppendLine("Анализ крови - 25р");
            builder.AppendLine("Анализ мочи - 30р");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}