using System.Collections.Generic;

namespace MedicationStringService.API.DTOs
{
    public class StatisticsDTO
    {
        public int TotalCount { get; set; }

        public int TotalDosageCount { get; set; }

        public IEnumerable<CountByBottleSizeDTO> PerBottleSize { get; set; }

        public IEnumerable<CountByMedicationIdDTO> PerMedicationId { get; set; }
    }
}