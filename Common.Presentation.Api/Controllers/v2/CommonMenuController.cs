using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Presentation.Api.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonMenuController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(string name)
        {

            return Ok();


        }
    }
}
