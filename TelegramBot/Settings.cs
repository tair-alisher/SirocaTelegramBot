using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using TelegramBot.Models;
using TelegramBot.Models.AppSettings;

namespace TelegramBot
{
    public static class Settings
    {
        public static IConfigurationRoot Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName).AddJsonFile("appsettings.json", false).Build();

        private static BotSettingsModel _botSettings;
        public static BotSettingsModel BotSettings
        {
            get
            {
                if (_botSettings != null)
                    return _botSettings;

                var botSettingsSection = Configuration.GetSection("BotSettings");
                var botSettings = new BotSettingsModel();
                botSettingsSection.Bind(botSettings);
                _botSettings = botSettings;

                return botSettings;
            }
        }

        private static MessageTemplatesModel _messageTemplates;
        public static MessageTemplatesModel MessageTemplates
        {
            get
            {
                if (_messageTemplates != null)
                    return _messageTemplates;

                var templatesSection = Configuration.GetSection("MessageTemplates");
                var templates = new MessageTemplatesModel();
                templatesSection.Bind(templates);
                _messageTemplates = templates;

                return templates;
            }
        }

        private static DefaultValuesModel _defaultValues;
        public static DefaultValuesModel DefaultValues
        {
            get
            {
                if (_defaultValues != null)
                    return _defaultValues;

                var defaultValuesSection = Configuration.GetSection("DefaultValues");
                var defaultValues = new DefaultValuesModel();
                defaultValuesSection.Bind(defaultValues);
                _defaultValues = defaultValues;

                return defaultValues;
            }
        }
    }
}