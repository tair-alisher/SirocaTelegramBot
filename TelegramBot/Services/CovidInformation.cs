using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services
{
    public static class CovidInformation
    {
        public static async Task SendCovidInformationMessage(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder("Общая информация о ковид и какие анализы делают в лаборатории для ковида");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}