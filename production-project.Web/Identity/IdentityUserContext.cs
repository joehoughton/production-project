namespace production_project.Web.Identity
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityUserContext : IdentityDbContext<IdentityUser>
    {
        public IdentityUserContext()
            : base("Sparebeds", throwIfV1Schema: false)
        {
        }

        public static IdentityUserContext Create()
        {
            return new IdentityUserContext();
        }
    }
}