﻿using static DsaGame.Web.Utility.SD;

namespace DsaGame.Web.Models.dtos
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
