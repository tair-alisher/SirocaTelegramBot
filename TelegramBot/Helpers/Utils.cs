using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Helpers
{
    public static class Utils
    {
        public static InlineKeyboardButton[][] GetSamplingPointsInlineKeyboard(string[] pointsNames)
        {
            var keyboardInline = new InlineKeyboardButton[pointsNames.Length][];
            for (var i = 0; i < pointsNames.Length; i++)
            {
                keyboardInline[i] = new[]
                {
                    InlineKeyboardButton.WithCallbackData(pointsNames[i], $"{Options.SamplingPoint}_{pointsNames[i]}")
                };
            }

            return keyboardInline;
        }
    }
}