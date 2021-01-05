namespace TelegramBot.Helpers
{
    public static class Options
    {
        public const string StartCommand = "/start";
        public const string FirstClinicInfoCommand = "/info1";
        public const string SecondClinicInfoCommand = "/info2";
        public const string ThirdClinicInfoCommand = "/info3";
        public const string ShowAppointmentList = "showappointmentlist";
        public const string MyPhoneNumber = "Мой номер телефона";
        public const string BloodPoint = "blood_point";

        public static readonly string ServicesSearch = $"{char.ConvertFromUtf32(	0x1F50E)} Поиск услуг (цены)";
        public static readonly string HandwritingDirection = $"{char.ConvertFromUtf32(0x1F648)} Разбор почерка направления";
        public static readonly string Results = $"{char.ConvertFromUtf32(0x2705)} Natijalar Результаты";
        public static readonly string CallMeBack = $"{char.ConvertFromUtf32(0x1F4F2)} Перезвонить мне";
        public static readonly string CallAnAmbulance = $"{char.ConvertFromUtf32(0x1F691)} Вызвать скорую";

        public static readonly string Appointment = $"{char.ConvertFromUtf32(0x1F4DD)} Запись на прием к врачу";
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
    }
}