using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Exceptions;
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

        public static async Task SendPdfWithTestResults(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Выполняет обработка запроса. Ожидайте");
            var sessionId = await ApiActions.RequestSessionId();
            if (string.IsNullOrEmpty(sessionId))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка");
                return;
            }

            try
            {
                var codeWord = message.Text.Split(' ').Last();
                var resultPdfString = await ApiActions.GetTestResults(sessionId, codeWord);
                var resultPdf = new InputOnlineFile(resultPdfString, "Результаты анализов.pdf");
                await botClient.SendDocumentAsync(message.Chat.Id, resultPdf, caption: "Результаты анализов",
                    replyMarkup: Common.GetCommonReplyKeyboardMarkup());
            }
            catch (TestResultsPdfNotFound)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Результаты анализов не найдены",
                    replyMarkup: Common.GetCommonReplyKeyboardMarkup());
            }
        }
    }
}