﻿using static MangoFood.UI.Utilities.SD;

namespace MangoFood.UI.Models.DTOs.Common
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
