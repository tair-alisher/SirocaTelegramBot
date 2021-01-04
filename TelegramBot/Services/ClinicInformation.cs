using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services
{
    public static class ClinicInformation
    {
        public static async Task SendInformationAboutClinic(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder("Базовая информаци о клинике");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}