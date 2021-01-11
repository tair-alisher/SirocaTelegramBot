﻿using System;
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
    public static class MedicalServicesPrice
    {
        public static async Task SendMedicalServicesPrice(ITelegramBotClient botClient, Message message)
        {
            var services = await ApiActions.GetMedicalServices();
            services = services.Take(10).ToList();

            var builder = new StringBuilder();
            builder.AppendLine("Цены на услуги врачей");
            builder.AppendLine();

            foreach (var service in services)
                builder.AppendLine($"{service.Name} - {service.Price} ({service.SpecialityName})\n");

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString());
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
                    new InputTextMessageContent($"({Options.MedicalServicesShortCut}) {service.Name}")));

            await botClient.AnswerInlineQueryAsync(inlineQuery.Id, results);
        }

        public static async Task SendMedicalServiceInfoAsync(ITelegramBotClient botClient, Message message)
        {
            var medicalServices = await ApiActions.GetMedicalServices();
            var serviceName = message.Text.Split(' ').Last();
            var service = medicalServices.First(s => s.Name == serviceName);

            await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Стоимость услуги '{service.Name}' {service.Price}");
        }
    }
}