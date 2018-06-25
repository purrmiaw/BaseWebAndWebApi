using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Extensions
{
    public class SharedTokenAuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
