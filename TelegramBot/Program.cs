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
using TelegramBot.Helpers;
using TelegramBot.Services;

namespace TelegramBot
{
    class Program
    {
        private static ITelegramBotClient _bot;

        static async Task Main()
        {
            _bot = new TelegramBotClient(Configuration.Token);

            var cancellationTokenSource = new CancellationTokenSource();

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

            Task action;
            if (message.Text.Equals(Options.StartCommand))
                action = Common.SendStartMessage(_bot, message);
            else if (message.Text.Equals(Options.ClinicInformation))
                action = ClinicInformation.SendInformationAboutClinic(_bot, message);
            else if (message.Text.Equals(Options.MobileLaboratory))
                action = MobileLaboratory.SendPhoneNumberRequestMessage(_bot, message);
            else if (message.Text.Equals(Options.BloodTestPoints))
                action = BloodTestPoints.SendBloodTestPointsLocations(_bot, message);
            else if (message.Text.Equals(Options.CallCenter))
                action = CallCenter.SendPhoneNumberRequestMessage(_bot, message);
            else if (message.Text.Equals(Options.LaboratoryServicesPrice))
                action = LaboratoryServicesPrice.SendLaboratoryServicesPrice(_bot, message);
            else if (message.Text.Equals(Options.MedicalServicesPrice))
                action = MedicalServicesPrice.SendMedicalServicesPrice(_bot, message);
            else if (message.Text.Equals(Options.FirstClinicInfoCommand))
                action = Appointment.SendClinicInformation_1(_bot, message);
            else if (message.Text.Equals(Options.SecondClinicInfoCommand))
                action = Appointment.SendClinicInformation_2(_bot, message);
            else if (message.Text.Equals(Options.ThirdClinicInfoCommand))
                action = Appointment.SendClinicInformation_3(_bot, message);
            else if (message.Text.Equals(Options.ServicesSearch))
                action = ServicesSearch.SendSearchInlineMarkup(_bot, message);
            else if (message.Text.Equals(Options.Appointment))
                action = Appointment.SendAppointmentInlineMarkup(_bot, message);
            else if (message.Text.Equals(Options.Results))
                action = Results.SendInstructionMessage(_bot, message);
            else if (message.Text.Equals(Options.HandwritingDirection) || message.Text.Equals(Options.CallAnAmbulance))
                action = Common.SendUserPhoneNumberRequiredMessage(_bot, message);
            else if (message.Text.Equals(Options.Cancel))
                action = Common.SendStartMessage(_bot, message);
            else
                action = Common.SendStartMessage(_bot, message);

            await action;

            // await _bot.SendTextMessageAsync(message.Chat.Id, $"Received: {message.Text}");
        }

        static async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            Task action;
            if (callbackQuery.Data.Equals(Options.ShowAppointmentList))
                action = Appointment.SendAppointmentInlineMarkup(_bot, callbackQuery);
            else if (callbackQuery.Data.Contains("appointment"))
                action = Appointment.SendClinicSelectionMarkup(_bot, callbackQuery);
            else if (callbackQuery.Data.Contains(Options.BloodPoint))
                action = BloodTestPoints.SendBloodTestPointLocation(_bot, callbackQuery);
            else
                action = Common.SendStartMessage(_bot, callbackQuery.Message);

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