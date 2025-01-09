namespace Refugee_manegment.Models
{
    public class HealthRecord
    {
        public int Id { get; set; }
        public int RefugeeId { get; set; }
        public string? MedicalCondition { get; set; }
        public DateTime DateOfRecord { get; set; }
        public string? TreatmentDetails { get; set; }

        public Refugee? Refugee { get; set; }

    }
}
