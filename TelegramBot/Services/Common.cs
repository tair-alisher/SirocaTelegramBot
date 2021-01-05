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
                    new KeyboardButton[] {Options.Appointment},
                    new KeyboardButton[] {Options.TestResults, Options.ClinicInformation},
                    new KeyboardButton[] {Options.MobileLaboratory, Options.Covid},
                    new KeyboardButton[] {Options.BloodTestPoints, Options.CallCenter},
                    new KeyboardButton[] {Options.LaboratoryServicesPrice, Options.MedicalServicesPrice}
                }, resizeKeyboard: true);


            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(),
                replyMarkup: generalKeyboardMarkup);
        }
    }
}