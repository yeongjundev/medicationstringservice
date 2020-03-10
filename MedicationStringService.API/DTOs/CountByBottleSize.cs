using MedicationStringService.API.Models;

namespace MedicationStringService.API.DTOs
{
    public class CountByBottleSize
    {
        public BottleSizeEnum BottleSize { get; set; }

        public int Count { get; set; }
    }
}