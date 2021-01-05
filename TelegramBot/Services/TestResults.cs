using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class TestResults
    {
        public static async Task SendInstructionMessage(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder("Введите номер заказа и кодовое слово в формате \"номер_заказа кодовое_слово\" без кавычек.");
            builder.AppendLine();
            builder.AppendLine("Пример: 123456 ABCD12");

            var cancelKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {Options.Cancel}
            }, resizeKeyboard: true);

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(), replyMarkup: cancelKeyboard);
        }
    }
}