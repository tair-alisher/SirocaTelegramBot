﻿using System.Collections.Generic;

namespace TelegramBot.Models
{
    public class ResponseModel<T>
    {
        public List<T> Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}