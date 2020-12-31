using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Classes
{
    public static class Start
    {
        public static async Task SendStartMessage(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);

            var builder = new StringBuilder();
            builder.AppendLine("Этот бот поможет вам узнать об услугах сети клиник Shox International Hospital.");
            builder.AppendLine("Чтобы начать нажмите /start.");
            builder.AppendLine("Чтобы получить помощь нажмите /help.");

            var generalKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[]
                    {
                        "\xD83D\xDCDD Запись",
                        $"{char.ConvertFromUtf32(0x2705)} Natijalar Результаты"
                    },
                    new KeyboardButton[]
                    {
                        "\xD83D\xDE48 Разбор почерка направления",
                        "\xD83D\xDD0E Поиск услуг (цены)"
                    },
                    new KeyboardButton[] {"\xD83D\xDCF2 Перезвонить мне"},
                    new KeyboardButton[] {"\xD83D\xDE91 Вызвать скорую"},
                }, resizeKeyboard: true);


            // await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(), replyMarkup: generalKeyboardMarkup);
        }
    }
}