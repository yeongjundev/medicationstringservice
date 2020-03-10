using System.Threading.Tasks;
using AutoMapper;
using MedicationStringService.API.Persistences;
using Microsoft.AspNetCore.Mvc;

namespace MedicationStringService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public StatisticsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // The total number of medications (MedicationStrings) that have been inputted.
            int totalMedicationStrings = await _uow.MedicationStringRepo.TotalCount();
            // The total dosage count of all medications (MedicationStrings).
            int totalDosageCounts = await _uow.MedicationStringRepo.TotalDosageCount();
            // The total number of medications (MedicationStrings) by bottle size.
            var totalNumberByBottleSize = await _uow.MedicationStringRepo.TotalNumberByBottleSize();
            // A list of individual medication Ids and the number of times each individual medication
            // (MedicationId) has been supplied.


            return Ok();
        }
    }
}