namespace MedicationStringService.API.Models
{
    public enum BottleSizeEnum
    {
        NA,
        S,
        M,
        L,
        XL,
        XXL
    }

    public class MedicationString
    {
        public int Id { get; set; }

        public string MedicationId { get; set; }

        public BottleSizeEnum BottleSize { get; set; }

        public int DosageCount { get; set; }
    }
}