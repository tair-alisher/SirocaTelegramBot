using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services
{
    public static class ServicesSearch
    {
        public static async Task SendSearchInlineMarkup(ITelegramBotClient botClient, Message message)
        {
            var searchInlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Поиск", "test")
                }
            });
            await botClient.SendTextMessageAsync(message.Chat.Id, "Нажмите на кнопку ниже для поиска",
                replyMarkup: searchInlineKeyboard);
        }
    }
}