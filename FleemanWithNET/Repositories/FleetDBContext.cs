using fleeman_with_dot_net.Models;

using Microsoft.EntityFrameworkCore;
//You’re using Entity Framework Core (Microsoft.EntityFrameworkCore namespace).
//This is the ORM (Object Relational Mapper) that lets you map C# classes → Database tables.


namespace fleeman_with_dot_net.Repositories
{
    public class FleetDBContext:DbContext
    {
        public FleetDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AirportDetails> airport_details { get; set; }
        public DbSet<CityDetails> city_details { get; set; }
        public DbSet<StateDetails> state_details { get; set; }
        public DbSet<LocationDetails> location_details { get; set; }
        public DbSet<CarDetails> car_details { get; set; }
        public DbSet<VehicleDetails> vehicle_details { get; set; }
        public DbSet<AddOnDetails> add_on_details { get;set; }
        public DbSet<MemberDetails> member_details { get; set; }
        public DbSet<BookingDetails> booking_details {  get; set; }
        public DbSet<StaffDetails> staff_details { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<AuthenticationTable> authentication_tables { get; set; }
        public DbSet<ApplicationUser> application_user { get; set; }

        public DbSet<BookingConfirmationDetails> booking_confirmation_details { get; set; }
        public DbSet<AddOnBook> add_on_book { get; set; }
        public DbSet<InvoiceHeaderTable> invoice_header_table { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure optional relationships
            modelBuilder.Entity<LocationDetails>()
                .HasOne(ld => ld.City)
                .WithMany()
                .HasForeignKey("City_Id");

            modelBuilder.Entity<LocationDetails>()
                .HasOne(ld => ld.State)
                .WithMany()
                .HasForeignKey("State_Id");
        }
    }
}
