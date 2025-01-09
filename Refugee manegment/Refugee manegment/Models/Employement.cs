namespace Refugee_manegment.Models
{
    public class Employement

    {
        public int Id { get; set; }
        public int RefugeeId { get; set; }
        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Refugee? Refugee { get; set; }

    }
}
