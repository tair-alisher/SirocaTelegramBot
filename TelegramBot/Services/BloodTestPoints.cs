using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Services
{
    public static class BloodTestPoints
    {
        private const string Chinabad = "chinabad";
        private const string Alaiskii = "alaiskii";
        private const string Evromed = "evromed";
        private const string Namangan = "namangan";

        public static async Task SendBloodTestPointsLocations(ITelegramBotClient botClient, Message message)
        {
            var builder = new StringBuilder("Пункты сдачи анализов");

            var locationsList = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("Чинабад 10а", $"{Options.BloodPoint}_{Chinabad}")},
                new[] {InlineKeyboardButton.WithCallbackData("Алайский", $"{Options.BloodPoint}_{Alaiskii}")},
                new[] {InlineKeyboardButton.WithCallbackData("Евромед", $"{Options.BloodPoint}_{Evromed}")},
                new[] {InlineKeyboardButton.WithCallbackData("Наманган", $"{Options.BloodPoint}_{Namangan}")}
            });

            await botClient.SendTextMessageAsync(message.Chat.Id, builder.ToString(), replyMarkup: locationsList);
        }

        public static async Task SendBloodTestPointLocation(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var bloodPoint = callbackQuery.Data.Split('_').Last();
            switch (bloodPoint)
            {
                case Chinabad:
                    await botClient.SendVenueAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 41.4163075f,
                        longitude: 69.4109129f,
                        title: "Чинабад",
                        address: "Чинобод, Узбекистан");
                    break;
                case Alaiskii:
                    await botClient.SendVenueAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 40.3333485f,
                        longitude: 73.6745782f,
                        title: "Алайский",
                        address: "Аюу-Тапан, Алай району");
                    break;
                case Evromed:
                    await botClient.SendVenueAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 40.9340053f,
                        longitude: 69.5808503f,
                        title: "Клиника \"EUROMED\"",
                        address: "Ташкент, Узбекистан");
                    break;
                case Namangan:
                    await botClient.SendVenueAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 40.9706421f,
                        longitude: 71.5045362f,
                        title: "Наманган",
                        address: "Наманган, Узбекистан");
                    break;
            }
        }
    }
}