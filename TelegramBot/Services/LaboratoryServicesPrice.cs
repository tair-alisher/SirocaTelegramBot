using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class LaboratoryServicesPrice
    {
        public static async Task SendLaboratoryServicesPrice(ITelegramBotClient botClient, Message message)
        {
            var services = await ApiActions.GetLaboratoryServices();
            services = services.Take(10).ToList();

            var builder = new StringBuilder();
            builder.AppendLine("Цены на услуги лаборатории");
            builder.AppendLine();

            foreach (var service in services)
                builder.AppendLine($"{service.Name} - {service.Price} ({service.SpecialityName})\n");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
        }

        public static async Task FindLaboratoryServices(ITelegramBotClient botClient, InlineQuery inlineQuery, string searchValue)
        {
            var laboratoryServices = await ApiActions.GetLaboratoryServices();
            var filteredServices = laboratoryServices
                .Where(s => s.Name.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            var results = new List<InlineQueryResultBase>();
            foreach (var service in filteredServices)
                results.Add(new InlineQueryResultArticle(service.Id.ToString(), service.Name,
                    new InputTextMessageContent($"{Options.LaboratoryServicesShortCut}_{service.Name}")));

            await botClient.AnswerInlineQueryAsync(inlineQuery.Id, results);
        }

        public static async Task SendLaboratoryServiceInfoAsync(ITelegramBotClient botClient, Message message)
        {
            var laboratoryServices = await ApiActions.GetLaboratoryServices();
            var serviceName = message.Text.Split(' ').Last();
            var service = laboratoryServices.First(s => s.Name == serviceName);

            await botClient.EditMessageTextAsync(message.Chat.Id, message.MessageId, $"Стоимость услуги '{service.Name}' {service.Price}");
        }
    }
}