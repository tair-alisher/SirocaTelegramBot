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
    public static class LaboratoryServicesPrice
    {
        public static async Task SendSearchLaboratoryServicesMarkup(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder("Нажмите на кнопку ниже для поиска услуги лаборатории");
            var searchKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Поиск услуг лаборатории", $"{Options.LaboratoryServicesShortCut} {Settings.DefaultValues.DefaultLaboratoryService}") }
            });

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(), replyMarkup: searchKeyboard);
        }

        public static async Task FindLaboratoryServices(ITelegramBotClient botClient, InlineQuery inlineQuery, string searchValue)
        {
            var laboratoryServices = await ApiActions.GetLaboratoryServices();
            var filteredServices = laboratoryServices
                .Where(s => s.Name.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            var results = new List<InlineQueryResultBase>();
            foreach (var service in filteredServices)
                results.Add(new InlineQueryResultArticle(service.Id.ToString(), service.Name,
                    new InputTextMessageContent($"{Options.LaboratoryServicesShortCut} {service.Name}")));

            await botClient.AnswerInlineQueryAsync(inlineQuery.Id, results, cacheTime: 0);
        }

        public static async Task SendLaboratoryServiceInfoAsync(ITelegramBotClient botClient, Message message)
        {
            var laboratoryServices = await ApiActions.GetLaboratoryServices();
            var serviceName = message.Text.Replace(Options.LaboratoryServicesShortCut, "").TrimStart();
            var service = laboratoryServices.First(s => s.Name == serviceName);

            await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
            await botClient.SendTextMessageAsync(message.Chat.Id,
                string.Format(Settings.MessageTemplates.ServicePrice, service.Name, service.Price));
        }
    }
}