namespace MedicationStringService.API.DTOs
{
    public class MedicationStringDTO
    {
        public int Id { get; set; }

        public string MedicationId { get; set; }

        public string BottleSize { get; set; }

        public int DosageCount { get; set; }
    }
}