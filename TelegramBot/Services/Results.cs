using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class Results
    {
        public static async Task SendInstructionMessage(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Введите id и пароль как указано в примерах.  id пробел пароль.");
            builder.AppendLine();
            builder.AppendLine("Пример: 123456 ABCD12");

            var cancelKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {Options.Cancel}
            }, resizeKeyboard: true);

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(),
                replyMarkup: cancelKeyboardMarkup);
        }
    }
}