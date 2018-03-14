using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using HeadHunter.API.Infrastructure.Requests.Service;
using HeadHunter.API.Models;
using HeadHunter.API.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToggleSys.API.Infrastructure.Logging;
using ToggleSys.API.Models.Requests.Service;
using ToggleSys.API.Models.Responses;

namespace HeadHunter.API.Controllers
{
    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly IMapper _mapper;
        private readonly ISubjectService _service;

        public SubjectController(IMapper mapper, ILogger<SubjectController> logger, ISubjectService service)
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _service.GetAll();
                return Ok(_mapper.Map<List<Subject>, List<SubjectResponseModel>>(result.ToList()));
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.LIST_ITEMS, e.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail([Required] string email)
        {
            try
            {
                var result = await _service.GetByEmail(email);
                return Ok(_mapper.Map<Subject, SubjectResponseModel>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.GET_ITEM, e.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([Required] int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(_mapper.Map<Subject, SubjectResponseModel>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.GET_ITEM, e.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubjectRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(LoggingEvents.INSERT_ITEM, ModelState.ToString());
                    return BadRequest(ModelState);
                }

                var result = await _service.Create(_mapper.Map<CreateSubjectRequest, Subject>(request));
                return Created($"{Request.GetUri()}/{result.Name}", _mapper.Map<Subject, SubjectResponseModel>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.INSERT_ITEM, e.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] int id, [FromBody] UpdateSubjectRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(LoggingEvents.UPDATE_ITEM, ModelState.ToString());
                    return BadRequest(ModelState);
                }

                var model = _mapper.Map<UpdateSubjectRequest, Subject>(request);
                var result = await _service.Update(id, model);

                return Ok(_mapper.Map<Subject, SubjectResponseModel>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.UPDATE_ITEM, e.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(LoggingEvents.DELETE_ITEM, ModelState.ToString());
                    return BadRequest(ModelState);
                }

                var result = await _service.Delete(id);
                return Ok(_mapper.Map<Subject, SubjectResponseModel>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(LoggingEvents.DELETE_ITEM, e.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}