using MedicationStringService.API.Models;

namespace MedicationStringService.API.DTOs
{
    public class TotalNumberByBottleSizeResult
    {
        public BottleSizeEnum BottleSize { get; set; }

        public int Count { get; set; }
    }
}