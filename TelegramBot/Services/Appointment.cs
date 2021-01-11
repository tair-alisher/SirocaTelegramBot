using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class Appointment
    {
        public static async Task SendAppointmentLink(ITelegramBotClient botClient, Message message)
        {
            var appointmentKeyboard =
                new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(Options.Appointment,
                    Settings.DefaultValues.AppointmentLink));
            await botClient.SendTextMessageAsync(message.Chat.Id, "Нажмите на кнопку ниже",
                replyMarkup: appointmentKeyboard);
        }
        static (StringBuilder builder, InlineKeyboardMarkup keyboardMarkup) GetAppointmentInlineMarkupData()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Выберите раздел на который вы хотите оставить заявку на запись.");
            builder.AppendLine();
            builder.AppendLine("International Hospital.");

            var appointmentInlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("МСКТ", "appointment mskt") },
                new[] { InlineKeyboardButton.WithCallbackData("МРТ", "appointment mri") },
                new[] { InlineKeyboardButton.WithCallbackData("УЗИ", "appointment ultrasound") },
                new[] { InlineKeyboardButton.WithCallbackData("Рентген", "appointment x-ray") },
                new[] { InlineKeyboardButton.WithCallbackData("Специалисты", "appointment specialists") }
            });

            return (builder, appointmentInlineKeyboard);
        }
    }
}