﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Extensions
{
    public class SharedRefreshTokenResponse
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
