namespace production_project.Domain.Organisations.Dto
{
    using production_project.Domain.Beds.Models.Enums;

    public class OrganisationSearchDto
    {
        public OrganisationSearchDto(decimal latitude, decimal longitude, int distance, int? age, Gender gender, Tier tier)
        {
            Latitude = latitude;
            Longitude = longitude;
            Distance = distance;
            Age = age;
            Gender = gender;
            Tier = tier;
        }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Distance { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public Tier Tier { get; set; }
        public bool AllAges { get; set; }
    }
}
