using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class MedicalServicesPrice
    {
        public static async Task SendSearchMedicalServicesMarkup(ITelegramBotClient botClient, Message message)
        {

            var builder = new StringBuilder("Нажмите на кнопку ниже для поиска услуги врача");
            var searchKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Поиск услуг врача", $"{Options.MedicalServicesShortCut} {Settings.DefaultValues.DefaultMedicalService}") }
            });

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(), replyMarkup: searchKeyboard);
        }

        public static async Task FindMedicalServices(ITelegramBotClient botClient, InlineQuery inlineQuery,
            string searchValue)
        {
            var medicalServices = await ApiActions.GetMedicalServices();
            var filteredServices = medicalServices
                .Where(s => s.Name.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            var results = new List<InlineQueryResultBase>();
            foreach (var service in filteredServices)
                results.Add(new InlineQueryResultArticle(service.Id.ToString(), service.Name,
                    new InputTextMessageContent($"{Options.MedicalServicesShortCut} {service.Name}")));

            await botClient.AnswerInlineQueryAsync(inlineQuery.Id, results);
        }

        public static async Task SendMedicalServiceInfoAsync(ITelegramBotClient botClient, Message message)
        {
            var medicalServices = await ApiActions.GetMedicalServices();
            var serviceName = message.Text.Replace(Options.MedicalServicesShortCut, "").TrimStart();
            var service = medicalServices.First(s => s.Name == serviceName);

            await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
            await botClient.SendTextMessageAsync(message.Chat.Id,
                string.Format(Settings.MessageTemplates.ServicePrice, service.Name, service.Price));
        }
    }
}