using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class Common
    {
        public static async Task SendStartMessage(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Этот бот поможет вам узнать об услугах сети клиник Shox International Hospital.");
            builder.AppendLine("Чтобы начать нажмите /start.");
            builder.AppendLine("Чтобы получить помощь нажмите /help.");

            var generalKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[] {Options.BloodTestPoints, Options.CallCenter},
                    new KeyboardButton[] {Options.LaboratoryServicesPrice, Options.MedicalServicesPrice},
                    new KeyboardButton[] {Options.Appointment, Options.Results},
                    new KeyboardButton[] {Options.HandwritingDirection, Options.ServicesSearch},
                    new KeyboardButton[] {Options.CallMeBack},
                    new KeyboardButton[] {Options.CallAnAmbulance},
                }, resizeKeyboard: true);


            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(),
                replyMarkup: generalKeyboardMarkup);
        }

        public static async Task SendUserPhoneNumberRequiredMessage(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder("Отправьте свой номер телефона в формате 998912345678 или отправьте контакт с телефонной книжки");

            var phoneNumberSendMarkup = new ReplyKeyboardMarkup(new[]
            {
                new[] {new KeyboardButton(Options.MyPhoneNumber) {RequestContact = true}},
                new KeyboardButton[] {Options.Cancel}
            }, resizeKeyboard: true);

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(),
                replyMarkup: phoneNumberSendMarkup);
        }
    }
}