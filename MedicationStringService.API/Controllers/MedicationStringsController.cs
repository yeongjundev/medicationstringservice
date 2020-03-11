using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MedicationStringService.API.DTOs;
using MedicationStringService.API.Helpers;
using MedicationStringService.API.Persistences;
using MedicationStringService.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MedicationStringService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationStringsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        private readonly IMedicationStringBuilder _msBuilder;

        public MedicationStringsController(IUnitOfWork uow, IMapper mapper, IMedicationStringBuilder msBuilder)
        {
            _uow = uow;
            _mapper = mapper;
            _msBuilder = msBuilder;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // Convert HttpRequestBodyStream to JObject.
            JObject jsonBody = await Request.Body.ToJObject();
            if (jsonBody == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            // Validate Json format via JSchema.
            if (!jsonBody.Validate(
                @"{
                    'description': 'Post MedicationStrings',
                    'type': 'object',
                    'properties': {
                        'medicationStrings': {
                            'type': ['array', 'string'],
                            'items': {
                                'type': 'string'
                            }
                        }
                    }
                }"))
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var medicationStrings = _msBuilder.Build(jsonBody.GetValue("medicationStrings"));

            try
            {
                _uow.MedicationStringRepo.AddRange(medicationStrings);
                await _uow.Complete();
            }
            catch
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            return Ok(_mapper.Map<IEnumerable<MedicationStringDTO>>(medicationStrings));
        }
    }
}