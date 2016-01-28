namespace production_project.Domain.Wards.Models
{
    using System.Collections.Generic;
    using production_project.Domain.Beds.Models;
    using production_project.Domain.Organisations.Models;

    public class Ward
    {
        public Ward()
        {
            Beds = new List<Bed>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganisationId { get; set; }
        public virtual ICollection<Bed> Beds { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}
