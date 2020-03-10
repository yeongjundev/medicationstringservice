using System.Threading.Tasks;
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

        public MedicationStringsController(IUnitOfWork uow)
        {
            _uow = uow;
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

            var builder = new MedicationStringBuilder(jsonBody.GetValue("medicationStrings"));
            var medicationStrings = builder.Build();

            return Ok();
        }


    }
}