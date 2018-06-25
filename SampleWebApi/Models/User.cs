using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Models
{
    public class User: IdentityUser
    {
        public string RefreshToken { get; set; }
    }
}
