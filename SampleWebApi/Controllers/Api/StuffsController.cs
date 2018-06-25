using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;

namespace SampleWebApi.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StuffsController : ControllerBase
    {
        private IEnumerable<Stuff> _stuffs;

        public StuffsController()
        {
            var stuffs = new List<Stuff>();
            stuffs.Add(new Stuff { Id = 1, Name = "Stuff 1", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/computer.png" });
            stuffs.Add(new Stuff { Id = 2, Name = "Stuff 2", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/lorry.png" });
            stuffs.Add(new Stuff { Id = 3, Name = "Stuff 3", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/cleaner.png" });
            stuffs.Add(new Stuff { Id = 4, Name = "Stuff 4", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/plumbing.png" });
            stuffs.Add(new Stuff { Id = 5, Name = "Stuff 5", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/welding.png" });
            stuffs.Add(new Stuff { Id = 6, Name = "Stuff 6", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/camera.png" });
            stuffs.Add(new Stuff { Id = 7, Name = "Stuff 7", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/carrepair.png" });
            stuffs.Add(new Stuff { Id = 8, Name = "Stuff 8", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/aircond.png" });
            stuffs.Add(new Stuff { Id = 9, Name = "Stuff 9", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/bike.png" });
            stuffs.Add(new Stuff { Id = 10, Name = "Stuff 10", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/computer.png" });
            stuffs.Add(new Stuff { Id = 11, Name = "Stuff 11", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/cleaner.png" });
            stuffs.Add(new Stuff { Id = 12, Name = "Stuff 12", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/aircond.png" });
            stuffs.Add(new Stuff { Id = 13, Name = "Stuff 13", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/welding.png" });
            stuffs.Add(new Stuff { Id = 14, Name = "Stuff 14", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/cleaner.png" });
            stuffs.Add(new Stuff { Id = 15, Name = "Stuff 15", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/welding.png" });
            stuffs.Add(new Stuff { Id = 16, Name = "Stuff 16", ImageUrl = "https://miaw.xyz/sampleapi/images/icons/computer.png" });

            _stuffs = stuffs;
        }

        [HttpGet]
        public IEnumerable<Stuff> GetAll()
        {
            return _stuffs;
        }

        [HttpGet("{id}", Name = "GetById")]
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