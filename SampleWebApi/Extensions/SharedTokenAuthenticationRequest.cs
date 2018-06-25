using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Extensions
{
    public class SharedTokenAuthenticationRequest
    {
        public string RefreshToken { get; set; }
        public string FirebaseToken { get; set; }
    }
}
