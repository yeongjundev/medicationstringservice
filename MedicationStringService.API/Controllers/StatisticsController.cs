using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MedicationStringService.API.DTOs;
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
            Task<int> totalMedicationStrings = _uow.MedicationStringRepo.TotalCount();
            Task<int> totalDosageCounts = _uow.MedicationStringRepo.TotalDosageCount();
            Task<List<CountByBottleSize>> totalNumberByBottleSize =
                _uow.MedicationStringRepo.TotalNumberByBottleSize();
            Task<List<CountByMedicationId>> distinctMedicationIdsWithCount =
                _uow.MedicationStringRepo.DistinctMedicationIds();

            Task.WaitAll(new Task[] {
                totalMedicationStrings,
                totalDosageCounts,
                totalNumberByBottleSize,
                distinctMedicationIdsWithCount
            });

            var result = new StatisticsResult
            {
                TotalCount = totalMedicationStrings.Result,
                TotalDosageCount = totalDosageCounts.Result,
                PerBottleSize = totalNumberByBottleSize.Result,
                PerMedicationId = distinctMedicationIdsWithCount.Result
            };
            return Ok(_mapper.Map<StatisticsDTO>(result));
        }
    }
}