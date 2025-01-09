


using Microsoft.EntityFrameworkCore;
using Refugee_manegment.Models;

public class WebDbContext: DbContext
    {

    public WebDbContext(DbContextOptions<WebDbContext> options) : base(options) {  }
     public DbSet<Employement> Employements { get; set; }

        public DbSet<HealthRecord> HealthyRecords { get; set; }

        public DbSet<Sponsorship> Sponsorships { get; set; }

    public DbSet<Refugee> refugee{ get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Address { get; set; } = default!;

}