using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class MedicalServicesPrice
    {
        public static async Task SendMedicalServicesPrice(ITelegramBotClient botClient, Message message)
        {
            var services = await ApiActions.GetMedicalServices();
            services = services.Take(10).ToList();

            var builder = new StringBuilder();
            builder.AppendLine("Цены на услуги врачей");
            builder.AppendLine();

            foreach (var service in services)
                builder.AppendLine(service.Information + "\n");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}