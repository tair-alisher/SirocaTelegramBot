using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class CovidInformation
    {
        public static async Task SendCovidInformationMessage(ITelegramBotClient botClient, Message message)
        {
            var information = await ApiActions.GetCovid19InformationAsync();
            var builder = new StringBuilder(information);

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}