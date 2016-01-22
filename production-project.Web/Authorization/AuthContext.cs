namespace production_project.Web.Authorization
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using production_project.Web.Entities;

    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
     
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }

}