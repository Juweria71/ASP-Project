namespace Refugee_manegment.Models
{
    public class Address

    {
        public int Id { get; set; }
        public int RefugeeId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public Refugee? Refugee { get; set; }

    }
}
