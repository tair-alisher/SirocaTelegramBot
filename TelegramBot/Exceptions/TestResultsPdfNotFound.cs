using System;

namespace TelegramBot.Exceptions
{
    public class TestResultsPdfNotFound : Exception
    {
        public TestResultsPdfNotFound() : base("Pdf results file not found") {}
    }
}