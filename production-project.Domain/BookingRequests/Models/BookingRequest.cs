namespace production_project.Domain.BookingRequests.Models
{
    using System;
    using production_project.Domain.Beds.Models;
    using production_project.Domain.BookingRequests.Models.Enums;
    using production_project.Domain.Patients.Models;
    using production_project.Domain.Users.Models;

    public class BookingRequest
    {
        public int Id { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public int UserDetailId { get; set; }
        public int OrganisationId { get; set; }
        public string Notes { get; set; }
        public int PatientId { get; set; }
        public DateTime DateRequested { get; set; }
        public int BedId { get; set; }
        public bool ClinicalInformation { get; set; }
        public virtual Bed Bed { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}
