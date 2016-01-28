namespace production_project.Domain.Organisations.Models
{
    using System.Data.Entity.Spatial;

    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual OrganisationType OrganisationType { get; set; }
        public virtual DbGeography GeoLocation { get; set; }
    }
}
