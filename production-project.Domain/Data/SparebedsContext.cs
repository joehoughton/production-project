namespace production_project.Domain.Data
{
    using System.Data.Entity;
    using production_project.Domain.Authorization.Mappings;
    using production_project.Domain.Authorization.Models;
    using production_project.Domain.Organisation.Mappings;
    using production_project.Domain.Organisation.Models;
    using production_project.Domain.Users.Mapping;
    using production_project.Domain.Users.Models;

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
        public DbSet<Client> Clients { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationType> OrganisationTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new ClientMap());
            modelBuilder.Configurations.Add(new OrganisationMap());
            modelBuilder.Configurations.Add(new OrganisationTypeMap());
            modelBuilder.Configurations.Add(new RefreshTokenMap());
            modelBuilder.Configurations.Add(new UserDetailMap());
        }
    }
}
