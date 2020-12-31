using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Classes;

namespace TelegramBot
{
    class Program
    {
        private static ITelegramBotClient _bot;

        static async Task Main()
        {
            _bot = new TelegramBotClient(Configuration.Token);

            var cancellationTokenSource = new CancellationTokenSource();

            // _bot.OnMessage += Bot_OnMessage;
            _bot.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cancellationTokenSource.Token);

            var botInfo = await _bot.GetMeAsync();
            Console.WriteLine($"Start listening for {botInfo.Username}");
            Console.ReadLine();

            cancellationTokenSource.Cancel();
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message),
                UpdateType.EditedMessage => BotOnMessageReceived(update.Message),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult),
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        static async Task BotOnMessageReceived(Message message)
        {
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;

            var filteredMessage = Configuration.FilterEmojiAndGetFirstWord(message.Text);

            var action = filteredMessage switch
            {
                "/start" => Start.SendStartMessage(_bot, message),
                "/info1" => Appointment.SendClinicInformation_1(_bot, message),
                "/info2" => Appointment.SendClinicInformation_2(_bot, message),
                "/info3" => Appointment.SendClinicInformation_3(_bot, message),
                "поиск" => ServicesSearch.SendSearchInlineMarkup(_bot, message),
                "запись" => Appointment.SendAppointmentInlineMarkup(_bot, message),
                "результаты" => Results.SendInstructionMessage(_bot, message),
                _ => Start.SendStartMessage(_bot, message)
            };
            await action;

            await _bot.SendTextMessageAsync(message.Chat.Id, $"Received: {message.Text}, {filteredMessage}");
        }

        static async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            var filteredMessage = Configuration.FilterEmojiAndGetFirstWord(callbackQuery.Data);
            var action = filteredMessage switch
            {
                "appointment" => Appointment.SendClinicSelectionMarkup(_bot, callbackQuery),
                "showappointmentlist" => Appointment.SendAppointmentInlineMarkup(_bot, callbackQuery),
                _ => null
            };
            if (action != null)
                await action;

            // await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Received: {callbackQuery.Data}, {filteredMessage}");
        }

        static Task BotOnInlineQueryReceived(InlineQuery inlineQuery)
        {
            Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");
            return Task.CompletedTask;
        }

        static Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
            return Task.CompletedTask;
        }

        static Task UnknownUpdateHandlerAsync(Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    }
}