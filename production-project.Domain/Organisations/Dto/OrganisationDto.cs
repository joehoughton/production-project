namespace production_project.Domain.Organisations.Dto
{
    public class OrganisationDto
    {
        public OrganisationDto(int id, string name, string type, int typeId, double? latitude, double? longitude)
        {
            Id = id;
            Name = name;
            Type = type;
            TypeId = typeId;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int TypeId { get; private set; }
        public string Type { get; private set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
