namespace TelegramBot.Helpers
{
    public static class Options
    {
        public const string StartCommand = "/start";
        public const string HelpCommand = "/help";
        public const string SendMyPhoneNumber = "Отправить мой номер телефона";
        public const string SamplingPoint = "blood_point";

        public static readonly string Appointment = $"{char.ConvertFromUtf32(0x1F4DD)} Онлайн запись";
        public static readonly string TestResults = $"{char.ConvertFromUtf32(0x2705)} Результаты анализов";
        public static readonly string ClinicInformation = $"{char.ConvertFromUtf32(0x2139)} Информация о клинике";
        public static readonly string MobileLaboratory = $"{char.ConvertFromUtf32(0x1F691)} Выездная лаборатория";
        public static readonly string Covid = $"{char.ConvertFromUtf32(0x1F637)} Информация по ковид19";
        public static readonly string BloodTestPoints = $"{char.ConvertFromUtf32(0x1F489)} Где сдать кровь";
        public static readonly string CallCenter = $"{char.ConvertFromUtf32(0x1F4F2)} Связаться с колл-центром";
        public static readonly string LaboratoryServicesPrice = $"{char.ConvertFromUtf32(0x1F3E5)} Цены на услуги лаборатории";
        public static readonly string MedicalServicesPrice = $"{char.ConvertFromUtf32(0x1F48A)} Цены на услуги врачей";

        public static readonly string Back = $"{char.ConvertFromUtf32(0x1F519)} Назад";
        public static readonly string Cancel = $"{char.ConvertFromUtf32(0x274C)} Отмена";

        public const string MobileLaboratorySendYourPhoneNumber =
            "(Выездная лаборатория) Отправьте номер телефона для обратной связи";

        public const string CallCenterSendYourPhoneNumber = "(Колл-центр) Отправьте номер телефона для обратной связи";

        public const string LaboratoryServicesShortCut = "lab";
        public const string MedicalServicesShortCut = "med";
    }
}