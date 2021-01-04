using System.Linq;
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
        public static async Task SendAppointmentInlineMarkup(ITelegramBotClient botClient, Message message)
        {
            var (builder, keyboardMarkup) = GetAppointmentInlineMarkupData();
            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(),
                replyMarkup: keyboardMarkup);
        }

        public static async Task SendAppointmentInlineMarkup(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var (builder, keyboardMarkup) = GetAppointmentInlineMarkupData();

            await botClient.EditMessageTextAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                builder.ToString(), replyMarkup: keyboardMarkup);
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

        public static async Task SendClinicSelectionMarkup(ITelegramBotClient botClient, CallbackQuery callbackQuery)
            {
                var builder = new StringBuilder();
                builder.AppendLine("Выберите клинику для более подробной информации.\n");
                builder.AppendLine("Shox Med Center (Ойбек)\nПодробнее о клинике /info1\n");
                builder.AppendLine("Shox International Hosptal (Аэропорт)\nПодробнее о клинике /info2\n");
                builder.AppendLine("Здоровая семья (Чиланзар)\nПодробнее о клинике /info3");

                InlineKeyboardMarkup selectionInlineKeyboard = null;
                var section = callbackQuery.Data.Split(' ').Last();
                switch (section)
                {
                    case "mskt":
                    case "mri":
                        selectionInlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            new[] {InlineKeyboardButton.WithCallbackData("Shox Med Center (Ойбек)")},
                            new[] {InlineKeyboardButton.WithCallbackData("Здоровая семья (Чилназар)")},
                            new[] {InlineKeyboardButton.WithCallbackData(Options.Back, Options.ShowAppointmentList),}
                        });
                        break;
                    case "ultrasound":
                        selectionInlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            new[] {InlineKeyboardButton.WithCallbackData("Shox Med Center (Ойбек)")},
                            new[] {InlineKeyboardButton.WithCallbackData("Здоровая семья (Чилназар)")},
                            new[] {InlineKeyboardButton.WithCallbackData("Shox International Hospital(Аэропорт)")},
                            new[] {InlineKeyboardButton.WithCallbackData(Options.Back, Options.ShowAppointmentList)}
                        });
                        break;
                    case "x-ray":
                        selectionInlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            new[] {InlineKeyboardButton.WithCallbackData("Shox Med Center (Ойбек)")},
                            new[] {InlineKeyboardButton.WithCallbackData("Здоровая семья (Чилназар)")},
                            new[] {InlineKeyboardButton.WithCallbackData(Options.Back, Options.ShowAppointmentList)}
                        });
                        break;
                    case "specialists":
                        builder = builder.Clear();
                        builder.AppendLine("Выберите специалиста");

                        selectionInlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            new[] {InlineKeyboardButton.WithCallbackData("Окулист")},
                            new[] {InlineKeyboardButton.WithCallbackData("Пульмонолог")},
                            new[] {InlineKeyboardButton.WithCallbackData("Протокол коронарографии")},
                            new[] {InlineKeyboardButton.WithCallbackData("Первичный осмотр в отделении")},
                            new[] {InlineKeyboardButton.WithCallbackData("Пластический хирург")},
                            new[] {InlineKeyboardButton.WithCallbackData("Протокол ТЛБАП")},
                            new[] {InlineKeyboardButton.WithCallbackData(Options.Back, Options.ShowAppointmentList), }
                        });
                        break;
                }

                await botClient.EditMessageTextAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                    builder.ToString(), replyMarkup: selectionInlineKeyboard);
            }

        public static async Task SendClinicInformation_1(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Shox Med Center.");
            builder.AppendLine("Узбекистан г.Ташкент  Мирабадский рн. ул.Айбека 34");
            builder.AppendLine("Телефон: 71-202-02-12");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }

        public static async Task SendClinicInformation_2(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Shox International Hospital.");
            builder.AppendLine("Узбекистан г. Ташкент Яккасарайский район. Ул. Кичик халка йули, 70а Ташкент (Международный Аэропорт)");
            builder.AppendLine("Телефон: 71-207-00-17");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }

        public static async Task SendClinicInformation_3(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Здоровая Семья.");
            builder.AppendLine("Учтепинский район, массив Чиланзар, 11-й квартал, 32");
            builder.AppendLine("Телефон: 71-276-11-25");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}