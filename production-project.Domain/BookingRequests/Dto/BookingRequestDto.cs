namespace production_project.Domain.BookingRequests.Dto
{
    using System;
    using production_project.Domain.Beds.Models.Enums;
    using production_project.Domain.BookingRequests.Models.Enums;

    public class BookingRequestDto
    {
        public int UserDetailId { get; set; }
        public int OrganisationId { get; set; }
        public string Notes { get; set; }
        public PatientDto Patient { get; set; }
        public int BedId { get; set; }
        public string OrganisationFrom { get; set; }
        public string OrganisationTo { get; set; }
        public DateTime DateRequested { get; set; }
        public string PasId { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public bool ClinicalInformation { get; set; }
        public bool Incoming { get; set; }
        public Tier Tier { get; set; }
    }
}
