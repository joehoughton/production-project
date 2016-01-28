namespace production_project.Domain.Beds.Dto
{
    using System.Collections.Generic;
    using production_project.Domain.Beds.Models.Enums;

    public class BedRequestDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public Tier Tier { get; set; }
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationType { get; set; }
        public List<string> OrganisationContact { get; set; }
    }
}
