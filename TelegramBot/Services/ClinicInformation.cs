using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class ClinicInformation
    {
        public static async Task SendInformationAboutClinic(ITelegramBotClient botClient, Message message)
        {
            var information = await ApiActions.GetClinicInformationAsync();
            var builder = new StringBuilder(information);

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}