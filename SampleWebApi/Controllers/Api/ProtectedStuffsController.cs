using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;

namespace SampleWebApi.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/protectedstuffs")]
    [ApiController]
    // To protect by token authentication, use this [Authorize] instead
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProtectedStuffsController : Controller
    {
        private IEnumerable<Stuff> _stuffs;

        public ProtectedStuffsController()
        {
            var stuffs = new List<Stuff>();
            stuffs.Add(new Stuff { Id = 1, Name = "Protected Stuff 1", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/computer.png" });
            stuffs.Add(new Stuff { Id = 2, Name = "Protected Stuff 2", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/lorry.png" });
            stuffs.Add(new Stuff { Id = 3, Name = "Protected Stuff 3", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/cleaner.png" });
            stuffs.Add(new Stuff { Id = 4, Name = "Protected Stuff 4", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/plumbing.png" });
            stuffs.Add(new Stuff { Id = 5, Name = "Protected Stuff 5", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/welding.png" });
            stuffs.Add(new Stuff { Id = 6, Name = "Protected Stuff 6", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/camera.png" });
            stuffs.Add(new Stuff { Id = 7, Name = "Protected Stuff 7", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/carrepair.png" });
            stuffs.Add(new Stuff { Id = 8, Name = "Protected Stuff 8", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/aircond.png" });
            stuffs.Add(new Stuff { Id = 9, Name = "Protected Stuff 9", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/bike.png" });
            stuffs.Add(new Stuff { Id = 10, Name = "Protected Stuff 10", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/computer.png" });
            stuffs.Add(new Stuff { Id = 11, Name = "Protected Stuff 11", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/cleaner.png" });
            stuffs.Add(new Stuff { Id = 12, Name = "Protected Stuff 12", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/aircond.png" });
            stuffs.Add(new Stuff { Id = 13, Name = "Protected Stuff 13", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/welding.png" });
            stuffs.Add(new Stuff { Id = 14, Name = "Protected Stuff 14", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/cleaner.png" });
            stuffs.Add(new Stuff { Id = 15, Name = "Protected Stuff 15", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/welding.png" });
            stuffs.Add(new Stuff { Id = 16, Name = "Protected Stuff 16", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/computer.png" });

            _stuffs = stuffs;
        }

        [HttpGet]
        public IEnumerable<Stuff> GetAll()
        {
            return _stuffs;
        }

        [HttpGet("{id}")]
        public ActionResult<Stuff> GetById(int id)
        {
            var stuff = _stuffs.Where(x => x.Id == id).FirstOrDefault();

            if (stuff is null)
            {
                return NotFound();
            }

            return stuff;
        }
    }
}