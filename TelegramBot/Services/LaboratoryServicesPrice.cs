using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class LaboratoryServicesPrice
    {
        public static async Task SendLaboratoryServicesPrice(ITelegramBotClient botClient, Message message)
        {
            var services = await ApiActions.GetLaboratoryServices();
            services = services.Take(10).ToList();

            var builder = new StringBuilder();
            builder.AppendLine("Цены на услуги лаборатории");
            builder.AppendLine();

            foreach (var service in services)
                builder.AppendLine(service.Information + "\n");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}