using System.Collections.Generic;

namespace MedicationStringService.API.DTOs
{
    public class StatisticsResult
    {
        public int TotalCount { get; set; }

        public int TotalDosageCount { get; set; }

        public IEnumerable<CountByBottleSize> PerBottleSize { get; set; }

        public IEnumerable<CountByMedicationId> PerMedicationId { get; set; }
    }
}