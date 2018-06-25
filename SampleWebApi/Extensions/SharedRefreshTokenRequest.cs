using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Extensions
{
    public class SharedRefreshTokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirebaseToken { get; set; }        
    }
}
