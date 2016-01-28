namespace production_project.Domain.Organisations.Dto
{
    using production_project.Domain.Beds.Models.Enums;

    public class OrganisationLocationDto
    {
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationType { get; set; }
        public decimal BedPrice { get; set; }
        public string BedType { get; set; }
        public Tier Tier { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Gender Gender { get; set; }
        public int BedId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Distance { get; set; }
    }
}