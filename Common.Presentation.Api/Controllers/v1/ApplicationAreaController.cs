using Common.Application.Core.Queries;
using Common.Application.Dto.Business.ApplicationArea;
using Common.Application.Dto.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Common.Presentation.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationAreaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicationAreaController> _logger;
        public ApplicationAreaController(IMediator mediator, ILogger<ApplicationAreaController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #region GET
        // GET: api/<ApplicationAreaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("This is a custom info");
            _logger.LogWarning("This is a costom warning");
            _logger.LogError("This is a custom error");

            return new string[] { "value1", "value2" };
        }

        // GET api/<ApplicationAreaController>/5
        [HttpGet("{db}/{id}")]
        public async Task<ActionResult<BaseResponse<List<ApplicationAreaDto>>>> GetAsync(string db, int id)
        {
            var applicationAreaDtoList = await _mediator.Send(new GetApplicationAreaListQuery() { ApplicationAreaId = id });
            return Ok(applicationAreaDtoList);
        }
        #endregion

        #region POST
        // POST api/<ApplicationAreaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        #endregion

        #region PUT
        // PUT api/<ApplicationAreaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        #endregion

        #region DELETE
        // DELETE api/<ApplicationAreaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
