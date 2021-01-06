using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class SamplingPoints
    {
        public static async Task SendSamplingPointsLocations(ITelegramBotClient botClient, Message message)
        {
            var samplingPoints = await ApiActions.GetSamplingPointsAsync();

            var builder = new StringBuilder("Пункты сдачи анализов");
            var locationsList =
                new InlineKeyboardMarkup(
                    Utils.GetSamplingPointsInlineKeyboard(samplingPoints.Data.Select(p => p.Name).ToArray()));

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(), replyMarkup: locationsList);
        }

        public static async Task SendSamplingPointLocation(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var samplingPointName = callbackQuery.Data.Split('_').Last();
            var samplingPoint = await ApiActions.GetSamplingPointByNameAsync(samplingPointName);

            await botClient.SendLocationAsync(
                chatId: callbackQuery.Message.Chat.Id,
                latitude: samplingPoint.Latitude,
                longitude: samplingPoint.Longitude);
        }
    }
}