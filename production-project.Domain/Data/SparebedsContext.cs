namespace production_project.Domain.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Reflection;

    using production_project.Domain.Beds.Models;
    using production_project.Domain.BookingRequests.Models;
    using production_project.Domain.Organisations.Models;
    using production_project.Domain.Patients.Models;
    using production_project.Domain.Users.Models;
    using production_project.Domain.Wards.Models;

    public class SparebedsContext : DbContext
    {
        static SparebedsContext()
        {
            Database.SetInitializer<SparebedsContext>(null);
        }

        public SparebedsContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationType> OrganisationTypes { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
