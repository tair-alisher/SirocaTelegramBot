using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Enums;
using TelegramBot.Helpers;
using TelegramBot.Services;

namespace TelegramBot
{
    class Program
    {
        private static ITelegramBotClient _bot;
        private static string _botUsername;

        static async Task Main()
        {
            _bot = new TelegramBotClient(Settings.BotSettings.Token);

            var cancellationTokenSource = new CancellationTokenSource();

            _bot.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cancellationTokenSource.Token);

            var botInfo = await _bot.GetMeAsync();
            _botUsername = botInfo.Username;
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
            switch (message.Type)
            {
                case MessageType.Text:
                    await Route(message);
                    break;
                case MessageType.Contact:
                    if (message.ReplyToMessage != null)
                        await SendPhoneNumber(message);
                    break;
            }

            // await _bot.SendTextMessageAsync(message.Chat.Id, $"Message. Received: {message.Text}, {message.Contact.PhoneNumber}");
        }

        static async Task Route(Message message)
        {
            Task action = null;
            if (message.Text.Equals(Options.StartCommand))
                action = Common.SendStartMessage(_bot, message);
            else if (message.Text.Equals(Options.TestResults))
                action = TestResults.SendInstructionMessage(_bot, message);
            else if (message.Text.Equals(Options.ClinicInformation))
                action = ClinicInformation.SendInformationAboutClinic(_bot, message);
            else if (message.Text.Equals(Options.MobileLaboratory))
                action = MobileLaboratory.SendPhoneNumberRequestMessage(_bot, message);
            else if (message.Text.Equals(Options.Covid))
                action = CovidInformation.SendCovidInformationMessage(_bot, message);
            else if (message.Text.Equals(Options.BloodTestPoints))
                action = SamplingPoints.SendSamplingPointsLocations(_bot, message);
            else if (message.Text.Equals(Options.CallCenter))
                action = CallCenter.SendPhoneNumberRequestMessage(_bot, message);
            else if (message.Text.Equals(Options.LaboratoryServicesPrice))
                action = LaboratoryServicesPrice.SendSearchLaboratoryServicesMarkup(_bot, message);
            else if (message.Text.Equals(Options.MedicalServicesPrice))
                action = MedicalServicesPrice.SendSearchMedicalServicesMarkup(_bot, message);
            else if (message.Text.Equals(Options.Cancel))
                action = Common.SendStartMessage(_bot, message);
            else if (message.ViaBot != null && message.ViaBot.Username == _botUsername)
                action = Common.SendMessageWithServicePrice(_bot, message);

            if (action != null)
                await action;
        }

        static async Task SendPhoneNumber(Message message)
        {
            var category = message.ReplyToMessage.Text == Options.MobileLaboratorySendYourPhoneNumber
                ? ServiceType.OnsiteLab
                : ServiceType.Common;

            var result = await ApiActions.SendPhoneNumber(message.Contact.PhoneNumber, category);
            if (result)
                await _bot.SendTextMessageAsync(message.Chat.Id, "С Вами свяжется наш оператор",
                    replyMarkup: Common.GetCommonReplyKeyboardMarkup());
            else
                await _bot.SendTextMessageAsync(message.Chat.Id, "Произошла ошибка",
                    replyMarkup: Common.GetCommonReplyKeyboardMarkup());
        }

        static async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            Task action;
            if (callbackQuery.Data.Equals(Options.ShowAppointmentList))
                action = Appointment.SendAppointmentInlineMarkup(_bot, callbackQuery);
            else if (callbackQuery.Data.Contains("appointment"))
                action = Appointment.SendClinicSelectionMarkup(_bot, callbackQuery);
            else if (callbackQuery.Data.Contains(Options.SamplingPoint))
                action = SamplingPoints.SendSamplingPointLocation(_bot, callbackQuery);
            else
                action = Common.SendStartMessage(_bot, callbackQuery.Message);

            await action;

            // await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"CallbackQuery. Received: {callbackQuery.Data}");
        }

        static async Task BotOnInlineQueryReceived(InlineQuery inlineQuery)
        {
            Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

            var text = inlineQuery.Query;
            if (text.Split(' ').Length > 1)
            {
                if (text.Contains(Options.LaboratoryServicesShortCut))
                {
                    var searchValue = text.Replace(Options.LaboratoryServicesShortCut, "").Trim();
                    await LaboratoryServicesPrice.FindLaboratoryServices(_bot, inlineQuery, searchValue);
                }
                else if (text.Contains(Options.MedicalServicesShortCut))
                {
                    var searchValue = text.Replace(Options.MedicalServicesShortCut, "").Trim();
                    await MedicalServicesPrice.FindMedicalServices(_bot, inlineQuery, searchValue);
                }
            }
        }

        static Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult)
        {
            Console.WriteLine($"Received chosen inline result: {chosenInlineResult.ResultId}");
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