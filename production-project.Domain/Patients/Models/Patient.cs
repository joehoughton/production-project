namespace production_project.Domain.Patients.Models
{
    using production_project.Domain.Beds.Models.Enums;

    public class Patient
    {
        public int Id { get; set; }
        public string PasId { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}

