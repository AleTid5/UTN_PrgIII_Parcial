using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parcial_Tidele.Services.Repository;

namespace Parcial_Tidele.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        // GET: api/Main
        [HttpGet, HttpOptions]
        [EnableCors("_myAllowSpecificOrigins")]
        public JsonResult Get()
        {
            return new JsonResult(new MagicianRepository().FindAll());
        }

        // GET: api/Main/5
        [HttpGet("{id}", Name = "Get"), HttpOptions]
        [EnableCors("_myAllowSpecificOrigins")]
        public JsonResult Get(int id)
        {
            if (id == 1)
                return new JsonResult(new HouseRepository().FindAll());

            return new JsonResult(new SpellRepository().FindAll());
        }

        // POST: api/Main
        [HttpPost, HttpOptions]
        [EnableCors("_myAllowSpecificOrigins")]
        public void Post([FromBody] string value)
        {
        }       
    }
}
