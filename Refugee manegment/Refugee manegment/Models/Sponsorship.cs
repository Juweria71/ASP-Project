namespace Refugee_manegment.Models
{
    public class Sponsorship
    {
        public int Id { get; set; }
        public int RefugeeId { get; set; }
        public string? SponsorName { get; set; }
        public string? SponsorContact { get; set; }
        public DateTime SponsorshipStartDate { get; set; }
        public DateTime? SponsorshipEndDate { get; set; }

        public Refugee? Refugee { get; set; }

    }
}
