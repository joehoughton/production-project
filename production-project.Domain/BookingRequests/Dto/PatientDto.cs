namespace production_project.Domain.BookingRequests.Dto
{
    using production_project.Domain.Beds.Models.Enums;

    public class PatientDto
    {
        public string PasId { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}
