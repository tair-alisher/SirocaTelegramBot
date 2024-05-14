using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class CallCenter
    {
        public static async Task SendPhoneNumberRequestMessage(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder(Options.CallCenterSendYourPhoneNumber);

            var keyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new[] {new KeyboardButton(Options.SendMyPhoneNumber) {RequestContact = true}},
                new KeyboardButton[] {Options.Cancel}
            }, resizeKeyboard: true);

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(), replyMarkup: keyboardMarkup);
        }
    }
}