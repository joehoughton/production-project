namespace production_project.Domain.BookingRequests
{
    using System;

    using production_project.Domain.Data;
    using System.Linq;

    using production_project.Domain.BookingRequests.Dto;
    using production_project.Domain.BookingRequests.Models;
    using production_project.Domain.BookingRequests.Models.Enums;
    using production_project.Domain.Patients.Models;

    public class BookingRequestRepository : IBookingRequestRepository
    {
        private readonly SparebedsContext _context;

        public BookingRequestRepository(SparebedsContext context)
        {
            _context = context;
        }

        public BookingRequestDto Create(BookingRequestDto bookingRequestDto, string currentUserId)
            {
            var user = _context.UserDetails.Single(x => x.AspNetUser.Id == currentUserId);

            var bookingRequest = new BookingRequest
            {
                OrganisationId = bookingRequestDto.OrganisationId,
                UserDetailId = user.Id,
                Notes = bookingRequestDto.Notes,
                BedId = bookingRequestDto.BedId,
                DateRequested = DateTime.Now,
                CurrentStatus = CurrentStatus.Requested,
                Patient = new Patient
                {
                    PasId = bookingRequestDto.Patient.PasId,
                    Age = bookingRequestDto.Patient.Age,
                    Gender = bookingRequestDto.Patient.Gender
                },
                ClinicalInformation = bookingRequestDto.ClinicalInformation
            };

            _context.BookingRequests.Add(bookingRequest);
            _context.SaveChanges();

            return bookingRequestDto;
        }
    }
}
